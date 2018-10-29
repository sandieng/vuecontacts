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
const loginServiceUrl = 'https://localhost:44393/api/login';

const myContactService = {
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

  searchContact(searchName) {
    searchName = searchName.replace("contact: ", "");

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
  },

  exportContact() {
    const exportContactUrl = contactServiceUrl + '/export';
    return new Promise((resolve) => {
      axios.get(exportContactUrl)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  // Login related service calls
  saveLogin(login) {
    return new Promise((resolve, reject) => {
      axios.post(loginServiceUrl, login)
        .then((response) => {
          resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  searchLogin(searchName) {
    searchName = searchName.replace("login: ", "");
    const searchUrl = loginServiceUrl + `/${searchName}`;
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

  listLogin() {
    const listLogintUrl = loginServiceUrl;
    return new Promise((resolve, reject) => {
      axios.get(listLogintUrl)
        .then((response) => {
          resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  updateLogin(login) {
    const updateLoginUrl = loginServiceUrl +`/${login.id}`;

    return new Promise((resolve) => {
      axios.put(updateLoginUrl, login)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  deleteLogin(id) {
    const deleteLoginUrl = loginServiceUrl +`/${id}`;

    return new Promise((resolve) => {
      axios.delete(deleteLoginUrl)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },
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


export default myContactService;
