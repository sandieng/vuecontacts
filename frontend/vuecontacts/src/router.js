import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from './views/Home.vue';
import About from './views/About.vue';
import ContactAdd from './components/ContactAdd.vue';
import Welcome from './components/Welcome.vue';

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
      path: '/contact/add',
      name: 'contactAdd',
      component: ContactAdd,
    },
    {
      path: '/welcome',
      name: 'welcome',
      component: Welcome,
    },
  ],
});
