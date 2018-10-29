<template>
 <v-container fluid>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ message }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>
 </v-container>


</template>

<script>
  import myContactService from './../service';

  export default {
    name: 'LoginExport',
    data() {
      return {
        showSnackbar: false,
        message: '',
        url: '',
      }
    },

    props: ['auth', 'authenticated'],

    methods: {
      base64ToArrayBuffer(data) {
        var binaryString = window.atob(data);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
          var ascii = binaryString.charCodeAt(i);
          bytes[i] = ascii;
        }
        return bytes;
      },

      saveByteArray(reportName, byte) {
        var blob = new Blob([byte], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        var fileName = reportName;
        link.download = fileName;
        link.click();
      }
    },

    beforeMount() {
      myContactService.exportLogin()
        .then((response) => {
            this.showSnackbar = true;
            this.message = 'Contact list exported.';
            this.url = response.data;

            var excelBuffer = this.base64ToArrayBuffer(response.data);
            this.saveByteArray("MyLogins.xlsx", excelBuffer);
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