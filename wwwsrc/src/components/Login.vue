<template>
    <div class="login">
        <form @submit.prevent="login">
            <input type="email" placeholder="email" v-model="loginUser.email">
            <input type="password" placeholder="password" v-model="loginUser.password">
            <button type="submit">Login</button>
        </form>
        <form @submit.prevent="register">
            <input type="string" placeholder="username" v-model="newUser.name">
            <input type="email" placeholder="email" v-model="newUser.email">
            <input type="password" placeholder="password" v-model="newUser.password">
            <button type="submit">Register</button>
        </form>
    </div>
</template>

<script>
export default {
  name: 'Login',
  data () {
    return {
        loginUser: {
            email: '',
            password: ''
        },
        newUser: {
            name: '',
            email: '',
            password: ''
        }
    }
  },
  methods: {
      login(){
          this.$store.dispatch('login', this.loginUser)
      },
      register(){
          this.$store.dispatch('register', this.newUser)
      }
  }
}
</script>

<style scoped>


</style>