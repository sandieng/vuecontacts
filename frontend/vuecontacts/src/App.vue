<template>
  <v-app id="vuecontacts">
    <main-menu></main-menu>
    <contact-footer></contact-footer>
  </v-app>
</template>

<script>
import MainMenu from './components/MainMenu.vue';
import ContactFooter from './components/Footer.vue';

export default {
  name: 'App',
  data: () => ({
  }),

  components: {
    MainMenu,
    ContactFooter,
  },

  props: {
  },
};
</script>
