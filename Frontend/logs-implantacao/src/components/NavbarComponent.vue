<template>
  <nav class="fixed top-0 z-50 w-full border-b border-slate-200 bg-white/80 backdrop-blur-md dark:border-slate-800 dark:bg-slate-950/80">
    <div class="mx-auto flex max-w-[1400px] items-center justify-between px-6 py-3">
      
      <div class="flex items-center gap-3 shrink-0">
        <div class="h-8 w-1 bg-blue-600 rounded-full"></div> <h1 class="text-lg font-semibold tracking-tight text-slate-900 dark:text-slate-100">
          Logs <span class="text-slate-400 mx-1">—</span> <span class="font-normal text-slate-500">Implantação</span>
        </h1>
      </div>

      <div class="relative flex-1 max-w-2xl mx-12">
        <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
          <svg class="h-4 w-4 text-slate-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
        </div>
        <input
          v-model="searchState.query"
          type="text"
          placeholder="Filtrar arquivos, linhas, cpfs, parcelas, mensagens ou data/hora"
          class="w-full bg-slate-100/50 dark:bg-slate-900 border border-slate-200 dark:border-slate-800 pl-10 pr-4 py-1.5 rounded-lg text-white transition-all focus:bg-white dark:focus:bg-slate-950 focus:ring-2 focus:ring-blue-500/20 focus:border-blue-500 outline-none"
        />
        <div class="absolute inset-y-0 right-0 flex items-center pr-3">
          <kbd class="hidden sm:inline-block px-1.5 py-0.5 text-[10px] font-medium text-slate-400 border border-slate-300 dark:border-slate-700 rounded bg-white dark:bg-slate-800">
            /
          </kbd>
        </div>
      </div>

      <div class="flex items-center gap-4">
        <div class="flex items-center gap-2 px-3 py-1 rounded-full bg-emerald-50 dark:bg-emerald-500/10 border border-emerald-100 dark:border-emerald-500/20">
          <span class="relative flex h-2 w-2">
            <span class="animate-ping absolute inline-flex h-full w-full rounded-full bg-emerald-400 opacity-75"></span>
            <span class="relative inline-flex rounded-full h-2 w-2 bg-emerald-500"></span>
          </span>
          <span class="text-xs font-medium text-emerald-700 dark:text-emerald-400 uppercase tracking-wider">Live</span>
        </div>
      </div>
      
    </div>
  </nav>
</template>


<script setup>
import { ref, watch } from 'vue';
import { searchState } from '../stores/store.js'
const searchQuery = ref("");

watch(searchQuery, (newValue) => {
  // Lógica de filtro para os logs
  console.log("Filtrando logs por:", newValue);
});
</script>