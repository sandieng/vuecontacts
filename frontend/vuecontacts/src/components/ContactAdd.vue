<template>
  <v-container fluid>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ showInfo }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="grey lighten-4 py-4 title">
          Create contact
        </v-card-title>

        <v-container grid-list-sm class="pa-4">
          <v-layout row wrap>
            <v-flex xs12 align-center justify-space-between>
              <v-layout align-center>
                <v-avatar size="40px" class="mr-3">
                  <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt="">
                </v-avatar>
                <v-text-field placeholder="Name" v-model="contact.name"></v-text-field>
              </v-layout>
            </v-flex>
            <v-flex xs6>
              <v-text-field prepend-icon="business" placeholder="Company" v-model="contact.company"></v-text-field>
            </v-flex>
            <v-flex xs6>
              <v-text-field placeholder="Job title" v-model="contact.jobTitle"></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-text-field prepend-icon="mail" placeholder="Email" v-model="contact.email"></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-text-field type="tel" prepend-icon="phone" placeholder="0000 000 000" mask="#### ### ###" v-model="contact.phone"></v-text-field>
            </v-flex>
            <v-flex xs12>
              <v-text-field
                prepend-icon="notes" placeholder="Notes" v-model="contact.notes"></v-text-field>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat color="primary" @click="backToHome()">Cancel</v-btn>
          <v-btn flat @click="saveContact(contact)">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import contactService from './../service';

export default {
  name: 'ContactAdd',
  data() {
    return {
      dialog: true,
      showSnackbar: false,
      showInfo: '',
      contact: {
        name: '',
        company: '',
        jobTitle: '',
        email: '',
        phone: '',
        notes: '',
      },
    };
  },

  props: ['auth', 'authenticated'],

  methods: {
    saveContact(contact) {
      if (this.authenticated) {
        var expiryTimestamp = +window.localStorage.getItem('expires_at');
        var currentTimestamp = new Date().getTime();

        if (expiryTimestamp < currentTimestamp) {
          this.auth.login();
        }

        contactService.saveContact(contact)
          .then((response) => {
            this.showSnackbar = true;
            this.showInfo = 'New contact added.';
          })
          .catch((error) => {
            this.showSnackbar = true;
            this.showInfo = error.response.data.message;

            if (error.response.status === 401) {
              this.auth.logout();
            }
          });
        this.sanitiseContactForm();
      } else {
        this.auth.login();
      }
    },

    sanitiseContactForm() {
      // this.contact.name = '';
      // this.contact. company = '';
      // this.contact.jobTitle = '';
      // this.contact.email = '';
      // this.contact.phone = '';
      // this.contact.notes = '';
      this.contact = {};
      console.log(this.contact);
    },

    backToHome() {
      this.$router.push('/');
    },
  },
};
</script>
