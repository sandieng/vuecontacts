import axios from 'axios';
axios.interceptors.request.use(function(config) {
    if (typeof window === 'undefined') return config;

        const token = window.localStorage.getItem('id_token');
        if (token) {
        config.headers.Authorization = 'Bearer ' + token;
        }

        return config;
    })

const contactServiceUrl = 'https://localhost:44393/api/contact';
const contactService = {
  saveContact(contact) {
    return new Promise((resolve, reject) => {
      axios.post(contactServiceUrl, contact)
        .then((response) => {
          resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  search(searchName) {
    const searchUrl = contactServiceUrl + `/${searchName}`;
    return new Promise((resolve, reject) => {
      axios.get(searchUrl)
        .then((response) => {
          resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  listContact() {
    const listContactUrl = contactServiceUrl;
    return new Promise((resolve, reject) => {
      axios.get(listContactUrl)
        .then((response) => {
          resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  updateContact(contact) {
    const updateContactUrl = contactServiceUrl +`/${contact.id}`;

    return new Promise((resolve) => {
      axios.put(updateContactUrl, contact)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  deleteContact(id) {
    const deleteContactUrl = contactServiceUrl +`/${id}`;

    return new Promise((resolve) => {
      axios.delete(deleteContactUrl)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  }
};

// const contactServiceUrl = 'http://localhost:3010/api/private';

// const contactService = {
//   saveContact(contact) {
//     return new Promise((resolve, reject) => {
//       axios.get(contactServiceUrl)
//         .then((response) => {
//           resolve(response);
//         })
//         .catch((error) => {
//           reject(error);
//         });
//     });
//   },
// };


export default contactService;
