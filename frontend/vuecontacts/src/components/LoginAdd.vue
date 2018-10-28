<template>
  <v-container fluid>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ message }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="grey lighten-4 py-4 title">
          Create login
        </v-card-title>

        <v-container grid-list-sm class="pa-4">
          <v-layout row wrap>
            <v-flex xs12 align-center justify-space-between>
              <v-layout align-center>
                <v-avatar size="40px" class="mr-3">
                  <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt="">
                </v-avatar>
                <v-text-field placeholder="Login name to create" v-model="login.name"></v-text-field>
              </v-layout>
            </v-flex>
            <v-flex xs12>
              <v-text-field
                prepend-icon="comment" placeholder="Description for the new login" v-model="login.notes"></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-text-field prepend-icon="business" placeholder="Login website" v-model="login.websiteUrl"></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-text-field prepend-icon="person" placeholder="User name to login for the site" v-model="login.loginName"></v-text-field>
            </v-flex>
            <v-flex xs6>
              <v-text-field prepend-icon="vpn_key" placeholder="User password to login for the site" :type="passwordMode" required v-model="login.loginPassword" ></v-text-field>
            </v-flex>
            <v-flex xs6>
              <v-icon @click="togglePassword()">{{padlockIcon}}</v-icon>
            </v-flex>
     
          </v-layout>
        </v-container>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat color="primary" @click="backToHome()">Cancel</v-btn>
          <v-btn flat @click="saveLogin(login)">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
  import myContactService from './../service';

  export default {
    name: 'LoginAdd',
    data() {
      return {
        dialog: true,
        showSnackbar: false,
        message: '',
        login: {
          name: '',
          notes: '',
          websiteUrl: '',
          loginName: '',
          loginPassword: '',
        },
        passwordMode: 'password',
        padlockIcon: 'lock_open'
      }
    },
  
    props: ['auth', 'authenticated'],

    methods: {
       togglePassword() {
              if (this.passwordMode === 'password') {
                this.passwordMode = 'text';
                this.padlockIcon = 'lock';
              }
              else {
                this.passwordMode = 'password';
                this.padlockIcon = 'lock_open';
              }
            },

      saveLogin(login) {
        if (this.authenticated) {
          var expiryTimestamp = +window.localStorage.getItem('expires_at');
          var currentTimestamp = new Date().getTime();

          if (expiryTimestamp < currentTimestamp) {
            this.auth.login();
          }

          // Validate inputs
          myContactService.saveLogin(login)
            .then((response) => {
              this.showSnackbar = true;
              this.message = 'New login added.';
              this.sanitiseLoginForm();
            })
            .catch((error) => {
              this.showSnackbar = true;
              this.message = error.response.data.message;

              if (error.response.status == 400) {
                this.message =this.extractErrorDetails(error.response.data);
              }

              if (error.response.status === 401) {
                this.auth.logout();
              }
            });
        } else {
          this.auth.login();
        }
      },

      sanitiseLoginForm() {
        this.login = {};
        console.log(this.login);
      },

      extractErrorDetails(errorDetails) {
        if (errorDetails.Name) {
          return errorDetails.Name;
        }

        if (errorDetails.LoginName) {
          return errorDetails.LoginName;
        }

        if (errorDetails.LoginPassword) {
          return errorDetails.LoginPassword;
        }
      },

      backToHome() {
          this.$router.push('/');
      },
    }
  }
</script>
