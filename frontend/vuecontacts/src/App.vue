<template>
  <v-app id="vuecontacts">
    <main-menu :auth="auth" :authenticated="authenticated"></main-menu>

    <router-view 
        @authChange="xyz"
        :auth="auth"
        :authenticated="authenticated"></router-view>

    <contact-footer></contact-footer>
  </v-app>
</template>

<script>
import AuthService from './auth/AuthService';

import MainMenu from './components/MainMenu.vue';
import ContactFooter from './components/Footer.vue';
import Callback from './components/Callback.vue';

const auth = new AuthService();

const {
  login, logout, authenticated, authNotifier,
} = auth;


export default {
  name: 'App',
  data() {
    authNotifier.on('authChange', (authState) => {
      this.authenticated = authState.authenticated;
    });
    return {
      auth,
      authenticated,
    };
  },

  components: {
    MainMenu,
    ContactFooter,
    Callback,
  },

  props: {},

  methods: {
    login,
    logout,
    xyz(val) {
      console.log(val);
    }
  },

  watch: {
    authChange(val) {
      this.authenticated = val;
    },
  },
};
</script>
