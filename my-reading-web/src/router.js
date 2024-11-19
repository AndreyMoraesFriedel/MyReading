import Vue from 'vue'
import Router from 'vue-router'
import Meta from 'vue-meta'

import TeladeLogin from './views/telade-login'
import Cadastro from './views/cadastro'
import Biblioteca from './views/biblioteca'
import Leitura from './views/leitura'
import Perfil from './views/perfil'
import NotFound from './views/not-found'
import './style.css'

Vue.use(Router)
Vue.use(Meta)

export default new Router({
  mode: 'history',
  routes: [
    {
      name: 'TeladeLogin',
      path: '/',
      component: TeladeLogin,
    },
    {
      name: 'Cadastro',
      path: '/cadastro',
      component: Cadastro,
    },
    {
      name: 'Biblioteca',
      path: '/biblioteca',
      component: Biblioteca,
    },
    {
      name: 'Leitura',
      path: '/leitura',
      component: Leitura,
    },
    {
      name: 'Perfil',
      path: '/perfil',
      component: Perfil,
    },
    {
      name: '404 - Not Found',
      path: '**',
      component: NotFound,
      fallback: true,
    },
  ],
})
