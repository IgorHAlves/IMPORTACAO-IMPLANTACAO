<template>
  <div class="min-h-screen bg-slate-50 dark:bg-slate-950 pt-20 pb-10 px-6">
    <div class="mx-auto max-w-[1400px]">
      
      <div class="flex justify-between items-end mb-6">
        <div>
          <h2 class="text-2xl font-bold text-slate-800 dark:text-white">Registros de Importação</h2>
          <p class="text-sm text-slate-500">Monitoramento em tempo real dos logs do sistema</p>
        </div>
        <div class="flex gap-3">
          <button @click="fetchLogs" class="p-2 text-slate-500 hover:bg-slate-200 dark:hover:bg-slate-800 rounded-lg transition-colors">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15" />
            </svg>
          </button>
          <span class="px-3 py-1 bg-blue-100 dark:bg-blue-900/30 text-blue-600 dark:text-blue-400 text-xs font-bold rounded-full flex items-center">
            {{ filteredLogs.length }} LOGS
          </span>
        </div>
      </div>

      <div class="overflow-hidden rounded-xl border border-slate-200 bg-white shadow-sm dark:border-slate-800 dark:bg-slate-900">
        <div class="overflow-x-auto">
          <table class="w-full text-left text-sm border-collapse">
            <thead class="bg-slate-50 dark:bg-slate-800/50 text-slate-600 dark:text-slate-400 border-b border-slate-200 dark:border-slate-800">
              <tr>
                <th class="px-4 py-3 font-semibold">ID</th>
                <th class="px-4 py-3 font-semibold">Arquivo</th>
                <th class="px-4 py-3 font-semibold">Linha</th>
                <th class="px-4 py-3 font-semibold">CPF</th>
                <th class="px-4 py-3 font-semibold">Parcela</th>
                <th class="px-4 py-3 font-semibold">Mensagem</th>
                <th class="px-4 py-3 font-semibold text-right">Data/Hora</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-100 dark:divide-slate-800">
              <tr v-for="log in filteredLogs" :key="log.id" class="hover:bg-blue-50/50 dark:hover:bg-blue-900/10 transition-colors">
                <td class="px-4 py-3 font-mono text-xs text-blue-600 dark:text-blue-400 font-bold">
                  #{{ log.id }}
                </td>
                <td class="px-4 py-3 font-medium text-slate-700 dark:text-slate-200">
                  {{ log.arquivo_nome }}
                </td>
                <td class="px-4 py-3 text-slate-500">
                  {{ log.linha }}
                </td>
                <td class="px-4 py-3 font-mono text-slate-600 dark:text-slate-400">
                  {{ log.cpf || '---' }}
                </td>
                <td class="px-4 py-3">
                   <span class="px-2 py-0.5 bg-slate-100 dark:bg-slate-800 rounded text-xs">
                     {{ log.parcela || 'N/A' }}
                   </span>
                </td>
                <td class="px-4 py-3 max-w-xs truncate text-slate-600 dark:text-slate-400" :title="log.mensagem">
                    {{ log.mensagem }}
                </td>
                <td class="px-4 py-3 text-right text-xs text-slate-500 font-mono">
                  {{ formatDate(log.data_hora) }}
                </td>
              </tr>
              
              <tr v-if="filteredLogs.length === 0">
                <td colspan="7" class="px-4 py-12 text-center text-slate-400">
                  Nenhum registro encontrado para sua busca.
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
// Importamos o estado que criamos para a pesquisa
import { searchState } from '../stores/store.js'; 

const logs = ref([]);

// Esta função observa o que você digita na Navbar e filtra a tabela na hora
const filteredLogs = computed(() => {
  const query = searchState.query.toLowerCase();
  
  if (!query) return logs.value;

  return logs.value.filter(log => {
    return (
      log.arquivo_nome?.toLowerCase().includes(query) ||
      log.mensagem?.toLowerCase().includes(query) ||
      log.cpf?.includes(query) ||
      log.id.toString().includes(query)
    );
  });
});

const fetchLogs = async () => {
  try {
    const response = await fetch('http://localhost:3000/logs');
    if (!response.ok) throw new Error('Erro na requisição');
    const data = await response.json();
    logs.value = data;
  } catch (error) {
    console.error("Erro ao buscar logs:", error);
  }
};

const formatDate = (dateString) => {
  const options = { 
    day: '2-digit', month: '2-digit', year: 'numeric',
    hour: '2-digit', minute: '2-digit', second: '2-digit' 
  };
  return new Date(dateString).toLocaleDateString('pt-BR', options);
};

onMounted(fetchLogs);
</script>