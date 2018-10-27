<template>
  <v-app id="vuecontacts">
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ message }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

    <!-- <main-menu :auth="auth" :authenticated="authenticated"></main-menu> -->
      <v-navigation-drawer :clipped="$vuetify.breakpoint.lgAndUp" v-model="drawer" fixed app>
      <v-list dense v-if="authenticated">
        <template v-for="item in items">
          <v-layout v-if="item.heading" :key="item.heading" row align-center>
            <v-flex xs6>
              <v-subheader v-if="item.heading">
                {{ item.heading }}
              </v-subheader>
            </v-flex>
            <v-flex xs6 class="text-xs-center">
              <a href="#!" class="body-2 black--text">EDIT</a>
            </v-flex>
          </v-layout>

          <v-list-group
            v-else-if="item.children"
            v-model="item.model"
            :key="item.text"
            :prepend-icon="item.model ? item.icon : item['icon-alt']"
            append-icon=""
          >
            <v-list-tile slot="activator">
              <v-list-tile-content>
                <v-list-tile-title>
                  {{ item.text }}
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>

            <v-list-tile v-for="(child, i) in item.children" :key="i" @click="false">
              <v-list-tile-action v-if="child.icon">
                <v-icon>{{ child.icon }}</v-icon>
              </v-list-tile-action>
              <v-list-tile-content >
                <router-link :to="child.route" style="text-decoration:none">
                  <v-list-tile-title class="menu-padding-left" >
                    {{ child.text }}
                  </v-list-tile-title>
                </router-link>

              </v-list-tile-content>
            </v-list-tile>
          </v-list-group>

          <v-list-tile v-else :key="item.text" @click="false">
            <v-list-tile-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-tile-action>

            <v-list-tile-content>
                <v-list-tile-title>
                  {{ item.text }}
                </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-toolbar :clipped-left="$vuetify.breakpoint.lgAndUp" color="blue darken-3" dark app fixed>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
        <span class="hidden-sm-and-down">My Contacts</span>
      </v-toolbar-title>
      <v-text-field flat solo-inverted hide-details 
                    prepend-inner-icon="search" 
                    label="Type in a contact name or login name to search then hit Enter ..." 
                    class="hidden-sm-and-down"
                    v-model="searchCondition"
                    @keyup.enter="goSearch()"
                    >

      </v-text-field>
      
      <v-tooltip top>
        <v-btn  slot="activator" fab color="pink" dark @click="contactAdd()">
          <v-icon >add</v-icon>
        </v-btn>
        <span>Add new contact</span>
      </v-tooltip>
      <v-tooltip top>
        <v-btn  slot="activator" fab color="green" dark @click="loginAdd()">
          <v-icon >add</v-icon>
        </v-btn>
        <span>Add new login </span>
      </v-tooltip>

      <v-spacer></v-spacer>

      <v-btn
          id="qsLoginBtn"
          class="btn btn-primary btn-margin"
          v-if="!authenticated"
          @click="login()">
            Log In
      </v-btn>

      <v-btn
        id="qsLogoutBtn"
        class="btn btn-primary btn-margin"
        v-if="authenticated"
        @click="logout()">
          Log Out
      </v-btn>

      <v-btn icon>
        <v-icon>apps</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>notifications</v-icon>
      </v-btn>
    </v-toolbar>

    <router-view 
        :auth="auth"
        :authenticated="authenticated"
        :contacts="contacts"
        ></router-view>

    <contact-footer></contact-footer>
  </v-app>
</template>

<script>
import AuthService from './auth/AuthService';

import myContactService from '@/service';
// import MainMenu from './components/MainMenu.vue';
import ContactSearch from './components/ContactSearch.vue';
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
      showSnackbar: false,
      message: '',
      searchCondition: '',
      contacts: [],
      dialog: false,
      drawer: null,
      items: [
        {
          icon: 'contacts',
          'icon-alt': 'contacts',

          text: 'Contacts',
          model: false,

          children: [
            { text: 'Add new', route: '/contact/add' },
            { text: 'List contacts', route: '/contact/list' },
            { text: 'Export contacts', route: '/contact/export' },
          ],
        },
        { icon: 'lock', 
          'icon-alt': 'lock',
          text: 'Logins',
          model: false,
         
          children: [
            { text: 'Add new', route: '/login/add' },
            { text: 'List logins', route: '/login/list' },
            { text: 'Export logins', route: '/login/export' },
          ],
        },
        { icon: 'content_copy', text: 'Duplicates' },
        {
          icon: 'keyboard_arrow_up',
          'icon-alt': 'keyboard_arrow_down',
          text: 'More',
          model: false,
          children: [
            { text: 'Import', route: '/welcome' },
            { text: 'Export', route: '/welcome' },
            { text: 'Print', route: '/welcome' },
            { text: 'Undo changes', route: '/welcome' },
            { text: 'Other contacts', route: '/welcome' },
          ],
        },
      ],
    };
    
  },

  components: {
    // MainMenu,
    ContactSearch,
    ContactFooter,
    Callback,
  },

  props: {},

  methods: {
    login,
    logout,
    goSearch() {
      myContactService.search(this.searchCondition)
         .then((response) => {
           this.contacts = response.data;
           this.$router.push({ name: 'contactSearch' })
          })
          .catch((error) => {
            if (error.response.status === 401) {
              this.auth.logout();
            }
          });
    },
    contactAdd() {
      this.$router.push({name: 'contactAdd'});
    },
    loginAdd() {
      this.$router.push({name: 'loginAdd'});
    }
  },

  watch: {
     authenticated(val) {
       console.log(val);
      },

    '$route' (to, from) {
      if (!this.auth.isAuthenticated()) {
        this.showSnackbar = true;
        this.message = 'Your session has timed out, please log in.';
        this.auth.logout();
      }
    }
  }
}

</script>

<style scoped>
  .menu-padding-left {
    padding-left: 50px;
  }
  .quick-button-padding-top {
    padding-bottom: 100px;
  }
</style>