<template>
  <v-container fluid>
    <!-- <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ showInfo }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar> -->

<v-flex xs12 align-center offset-sm2>
    <v-data-table
        :headers="headers"
        :items="logins"
        :pagination.sync="pagination"
        class="elevation-1 paddingtop" 
    >
        <template slot="items" slot-scope="props">
        <td>{{ props.item.id }}</td>
        <td>{{ props.item.name }}</td>
        <td class="text-xs-left">{{ props.item.notes }}</td>
        <td>{{ props.item.websiteUrl}}</td>
        <td>{{ props.item.loginName}}</td>
        <td>{{ props.item.loginPassword}}</td>
        </template>
    </v-data-table>
</v-flex>
  </v-container>
</template>

<script>
import myContactService from './../service';

export default {
  name: 'LoginSearch',
  data() {
      return {
        pagination: {
        },
        headers: [
            {
            text: 'Login\'s ID',
            align: 'left',
            sortable: false,
            value: 'id'
            },
            { text: 'Login\'s Name', value: 'name' },

            { text: 'Notes', value: 'notes' },
            { text: 'Website', value: 'websiteUrl' },
            { text: 'User Name', value: 'loginName' },
            { text: 'User Password', value: 'loginPassword' },
        ],
      }
  },
  props: ['logins'],
  beforeMount() {
      
  }
}
</script>

<style scoped>
.paddingtop {
  padding-top: 50px;
}
</style>