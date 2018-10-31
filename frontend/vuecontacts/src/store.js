import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    newContacts: [],
    newLogins: [],
  },

  actions: {
    addNewContactToNotification: (context, payload) => {
      context.commit('addNewContact', payload);
    },
    addNewLoginToNotification: (context, payload) => {
      context.commit('addNewLogin', paylopad);
    }
  },

  mutations: {
    addNewContact: (state, payload) => state.newContacts.push(payload),
    addNewLogin: (state, payload) => state.newLogins.push(payload)
  },

  getters: {
    getNewContacts(state) {
      return state.newContacts;
    },
    getNewLogins(state) {
      return state.newLogins;
    },
    getNewContactsCount(state) {
      return state.newContacts.length;
    },
    getNewLoginsCount(state) {
      return state.newLogins.length;
    }
  }
});
