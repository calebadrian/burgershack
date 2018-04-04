<template>
  <div class="home">
    <h1>Hello {{user.name}}</h1>
  </div>
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      
    }
  },
  computed: {
    user(){
      return this.$store.state.user
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  
</style>
