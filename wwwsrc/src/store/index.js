import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//something.herokuapp.com/' : '//localhost:5000/'

var ourAPI = axios.create({
    baseURL: baseUrl + 'api/',
    timeout: 5000,
    withCredentials: true
})

var auth = axios.create({
    baseURL: baseUrl + 'account/',
    timeout: 5000,
    withCredentials: true
})

vue.use(vuex)

export default new vuex.Store({
    state: {
        user: {}
    },
    mutations: {
        setUser(state, payload){
            state.user = payload;
        }
    },
    actions: {
        login({ commit, dispatch }, payload) {
            auth.post('login', payload)
                .then(res => {
                    console.log(res.data)
                    commit('setUser', res.data)
                    router.push({name: "Home"})
                })
                .catch(err => {
                    console.error(err)
                })
        },
        register({ commit, dispatch }, payload) {
            auth.post('register', payload)
                .then(res => {
                    delete res.data.Id
                    delete res.data.Name
                    res.data.Password = payload.password
                    dispatch('login', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        }
    }
})