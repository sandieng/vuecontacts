<template>
 <v-container fluid>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ message }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

  <v-flex  xs12 align-center justify-space-between offset-sm2>
     <v-card class="paddingtop">
        <v-card-title class="grey lighten-4 py-4 title">
          Contact list location: 
           <div>
             <span>&nbsp;</span>
             <a :href="url">{{url}}</a>
          </div>
        </v-card-title>
       
     </v-card>
  </v-flex>
 </v-container>


</template>

<script>
  import myContactService from './../service';

  export default {
    name: 'ContactExport',
    data() {
      return {
        showSnackbar: false,
        message: '',
        url: '',
      }
    },

    props: ['auth', 'authenticated'],

    methods: {

    },

    beforeMount() {
      myContactService.exportContact()
       .then((response) => {
            this.showSnackbar = true;
            this.message = 'Contact list exported.';
            this.url = response.data;
          })
          .catch((error) => {
            this.showSnackbar = true;
            this.message = error.response.data.message;

            if (error.response.status === 401) {
              this.auth.logout();
            }
          });
    }
  }
</script>


<style scoped>
.paddingtop {
  padding-top: 100px;
}
</style>