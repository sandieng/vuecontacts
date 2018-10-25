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
        :items="contacts"
        :pagination.sync="pagination"
        class="elevation-1 paddingtop" 
    >
        <template slot="items" slot-scope="props">
        <td>{{ props.item.id }}</td>
        <td>{{ props.item.name }}</td>
        <td class="text-xs-left">{{ props.item.company }}</td>
        <td>{{ props.item.jobTitle}}</td>
        <td>{{ props.item.email}}</td>
        <td>{{ props.item.phone}}</td>
        <td>{{ props.item.notes}}</td>
        </template>
    </v-data-table>
</v-flex>
  </v-container>
</template>

<script>
import contactService from './../service';

export default {
  name: 'ContactSearch',
  data() {
      return {
        pagination: {
        },
        headers: [
            {
            text: 'Contact\'s ID',
            align: 'left',
            sortable: false,
            value: 'id'
            },
            { text: 'Contact\'s Name', value: 'name' },

            { text: 'Company', value: 'company' },
            { text: 'Job Title', value: 'jobTitle' },
            { text: 'Email', value: 'email' },
            { text: 'Phone', value: 'phone' },
            { text: 'Notes', value: 'notes' }
        ],
      }
  },
  props: ['contacts'],
  beforeMount() {
      
  }
}
</script>

<style scoped>
.paddingtop {
  padding-top: 50px;
}
</style>