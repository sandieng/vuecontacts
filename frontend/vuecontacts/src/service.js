import axios from 'axios';
// axios.interceptors.request.use(function(config) {
//     if (typeof window === 'undefined') return config;

//         const token = window.localStorage.getItem('jwtToken');
//         if (token) {
//         config.headers.Authorization = token;
//         }

//         return config;
//     })

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
};


export default contactService;
