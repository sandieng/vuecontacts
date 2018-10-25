import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from './views/Home.vue';
import About from './views/About.vue';
import ContactAdd from './components/ContactAdd.vue';
import ContactSearch from './components/ContactSearch.vue';
import ContactList from './components/ContactList.vue';
import Welcome from './components/Welcome.vue';
import Callback from './components/Callback.vue';

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
      path: '/welcome',
      name: 'welcome',
      component: Welcome,
    },
  ],
});
