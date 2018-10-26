<template>
  <div>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ message }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

    <v-toolbar flat color="white">
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                  <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.name" label="Contact Name"></v-text-field>
                </v-flex>
                  <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.company" label="Company"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.jobTitle" label="Job Title"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.email" label="Email"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.phone" label="Phone"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.notes" label="Notes"></v-text-field>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-toolbar>
    <v-data-table
      :headers="headers"
      :items="contactList"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td>{{ props.item.name }}</td>
        <td class="text-xs-left">{{ props.item.company }}</td>
        <td>{{ props.item.jobTitle}}</td>
        <td>{{ props.item.email}}</td>
        <td>{{ props.item.phone}}</td>
        <td>{{ props.item.notes}}</td>
        <td class="justify-center layout px-0">
          <v-icon
            small
            class="mr-2"
            @click="editContact(props.item)"
          >
            edit
          </v-icon>
          <v-icon
            small
            @click="deleteContact(props.item)"
          >
            delete
          </v-icon>
        </td>
      </template>
     
    </v-data-table>
  </div>
</template>

<script>
    import contactService from './../service';

    export default {
        name: 'CotactList',
        data() {
            return {
                showSnackbar: false,
                message: '',
                dialog: false,
                  headers: [
               
                    { text: 'Contact\'s Name', value: 'name' },

                    { text: 'Company', value: 'company' },
                    { text: 'Job Title', value: 'jobTitle' },
                    { text: 'Email', value: 'email' },
                    { text: 'Phone', value: 'phone' },
                    { text: 'Notes', value: 'notes' }
                ],
                contactList: [],   
                editedIndex: -1,
                editedItem: {
                dateJoined: null,
                rosteredDate: null
                } 
            }
        },
        watch: {
            dialog (val) {
                val || this.close()
            }
        },
        beforeMount() {
            contactService.listContact()
                .then((response) => {
                    this.error = false;
                    this.contactList = response.data.payload;
                })
                .catch((error) => {
                    this.error = true;
                    this.errorMessage = error.response.data.message;
                })
        },
        methods: {
           editContact(contact) {
                this.editedIndex = this.contactList.indexOf(contact);
                this.editedItem = Object.assign({}, contact);
                this.dialog = true;
            },

            deleteContact(contact) {
              const index = this.contactList.indexOf(contact)
              let ok = confirm('Are you sure you want to delete this contact?') && this.contactList.splice(index, 1);
              if (ok) {
                contactService.deleteContact(contact.id)
                  .then((response) => {
                      this.showSnackbar = true;      
                      this.message = 'Contact deleted.';
                    })
                    .catch((showSnackbar) => {
                      this.showSnackbar = true;
                      this.message = showSnackbar.response.data.message;
                });
              }
            },
          
            save () {
                if (this.editedIndex > -1) {
                    Object.assign(this.contactList[this.editedIndex], this.editedItem);

                    // Update change in the backend
                    let contact = {id: this.editedItem.id, 
                                name: this.editedItem.name, 
                                company: this.editedItem.company, 
                                jobTitle: this.editedItem.jobTitle,
                                email: this.editedItem.email,
                                phone: this.editedItem.phone,
                                notes: this.editedItem.notes
                                };
                    contactService.updateContact(contact)
                    .then(() => {
                            this.showSnackbar = true;      
                            this.message = 'Contact\'s data updated.';
                        })
                        .catch((error) => {
                            this.showSnackbar = true;
                            this.message = error.response.data.message;
                        });
                } else {
                    this.contactList.push(this.editedItem);
                }

                this.close()
            },

            close () {
                this.dialog = false
                setTimeout(() => {
                this.editedItem = Object.assign({}, this.defaultItem);
                this.editedIndex = -1;
                }, 300)
            },
        }
    }
</script>