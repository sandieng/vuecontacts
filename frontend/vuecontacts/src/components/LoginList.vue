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
                  <v-flex xs12>
                  <v-text-field v-model="editedItem.name" label="Login Name"></v-text-field>
                </v-flex>
                  <v-flex xs12>
                  <v-text-field v-model="editedItem.notes" label="Notes"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="editedItem.websiteUrl" label="Website"></v-text-field>
                </v-flex>
                <v-flex xs12>
                  <v-text-field v-model="editedItem.loginName" label="Login Name"></v-text-field>
                </v-flex>
                <v-flex xs6>
                  <v-text-field v-model="editedItem.loginPassword" label="Login Password" :type="passwordMode">
                  </v-text-field>
                </v-flex>
                <v-flex xs6>
                      <v-icon @click="togglePassword()">{{padlockIcon}}</v-icon>
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
    <v-flex xs12 align-center offset-sm2>
      <v-data-table
        :headers="headers"
        :items="loginList"
        hide-actions
        class="elevation-1"
      >
        <template slot="items" slot-scope="props">
          <td>{{ props.item.name }}</td>
          <td>{{ props.item.notes}}</td>
          <td><a :href="props.item.webSiteUrl" target="_blank">{{ props.item.webSiteUrl}}</a></td>
          <td>{{ props.item.loginName}}</td>
           
          <td v-if="passwordMode === 'text'">
            {{ props.item.loginPassword}}
              <v-icon @click="togglePassword()">{{padlockIcon}}</v-icon>
          </td>
          
          <td v-else>...<v-icon @click="togglePassword()">{{padlockIcon}}</v-icon></td>

          <td class="justify-center layout px-0">
            <v-icon
              small
              class="mr-2"
              @click="editLogin(props.item)"
            >
              edit
            </v-icon>
            <v-icon
              small
              @click="deleteLogin(props.item)"
            >
              delete
            </v-icon>
          </td>
        </template>
      </v-data-table>
    </v-flex>
  </div>
</template>

<script>
    import myLoginService from './../service';

    export default {
        name: 'LoginList',
        data() {
            return {
                showSnackbar: false,
                message: '',
                dialog: false,
                  headers: [
               
                    { text: 'Login\'s Name', value: 'name' },
                    { text: 'Notes', value: 'notes' },

                    { text: 'Website', value: 'webSiteUrl' },
                    { text: 'User Name', value: 'loginName' },
                    { text: 'User Password', value: 'loginPassword' },
                ],
                loginList: [],   
                editedIndex: -1,
                editedItem: {
                },
                passwordMode: 'password',
                padlockIcon: 'lock_open'
            }
        },
        watch: {
            dialog (val) {
                val || this.close()
            }
        },
        beforeMount() {
            myLoginService.listLogin()
                .then((response) => {
                    this.error = false;
                    this.loginList = response.data.payload;
                })
                .catch((error) => {
                    this.error = true;
                    this.errorMessage = error.response.data.message;
                })
        },
        computed: {
       
        },
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

           editLogin(login) {
                this.editedIndex = this.loginList.indexOf(login);
                this.editedItem = Object.assign({}, login);
                this.dialog = true;
            },

            deleteLogin(login) {
              const index = this.loginList.indexOf(login)
              let ok = confirm('Are you sure you want to delete this login?') && this.loginList.splice(index, 1);
              if (ok) {
                myLoginService.deleteLogin(login.id)
                  .then((response) => {
                      this.showSnackbar = true;      
                      this.message = 'Login deleted.';
                    })
                    .catch((showSnackbar) => {
                      this.showSnackbar = true;
                      this.message = showSnackbar.response.data.message;
                });
              }
            },
          
            save () {
                if (this.editedIndex > -1) {
                    Object.assign(this.loginList[this.editedIndex], this.editedItem);

                    // Update change in the backend
                    let login = {id: this.editedItem.id, 
                                name: this.editedItem.name, 
                                notes: this.editedItem.notes, 
                                websiteUrl: this.editedItem.websiteUrl,
                                loginName: this.editedItem.loginName,
                                loginPassword: this.editedItem.loginPassword,
                                };

                    myLoginService.updateLogin(login)
                      .then(() => {
                            this.showSnackbar = true;      
                            this.message = 'Login\'s data updated.';
                        })
                      .catch((error) => {
                          this.showSnackbar = true;
                          this.message = error.response.data.message;
                      });
                } else {
                    this.loginList.push(this.editedItem);
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