import Vue from 'vue';
import VueRouter from 'vue-router';

import Callback from './components/Callback.vue';
import Welcome from './components/Welcome.vue';
import Home from './views/Home.vue';
import About from './views/About.vue';

import ContactAdd from './components/ContactAdd.vue';
import ContactSearch from './components/ContactSearch.vue';
import ContactList from './components/ContactList.vue';
import ContactExport from './components/ContactExport.vue';

import LoginAdd from './components/LoginAdd.vue';
import LoginList from './components/LoginList.vue';
import LoginSearch from './components/LoginSearch.vue';
import LoginExport from './components/LoginExport.vue';


Vue.use(VueRouter);

export default new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/home',
      name: 'home',
      component: Home,
    },
    {
      path: '/welcome',
      name: 'welcome',
      component: Welcome,
    },
    {
      path: '/about',
      name: 'about',
      component: About,
    },
    {
      path: '/callback',
      name: 'Callback',
      component: Callback,
    },
    {
      path: '/contact/add',
      name: 'contactAdd',
      component: ContactAdd,
    },
    {
      path: '/contact/search',
      name: 'contactSearch',
      component: ContactSearch,
    },
    {
      path: '/contact/list',
      name: 'contactList',
      component: ContactList,
    },
    {
      path: '/contact/export',
      name: 'contactExport',
      component: ContactExport,
    },
    {
      path: '/login/add',
      name: 'loginAdd',
      component: LoginAdd,
    },
    {
      path: '/login/list',
      name: 'loginList',
      component: LoginList,
    },
    {
      path: '/login/search',
      name: 'loginSearch',
      component: LoginSearch,
    },
    {
      path: '/login/export',
      name: 'loginExport',
      component: LoginExport,
    },
  ],
});
