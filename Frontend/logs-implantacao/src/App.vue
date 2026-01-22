<script>
import NavbarComponent from './components/NavbarComponent.vue'

export default {
  name: 'App',
  components: {
    NavbarComponent
  }
}
</script>


<template>
  <div>
    <NavbarComponent />
    <router-view /> 
  </div>
</template>

