<template>
  <div id="login" class="text-center">
    <v-form class="form-signin" @submit.prevent="login">
      <v-container>
      <h1 class="h3 mb-3 font-weight-normal primary">Please Sign In</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <v-row>
      <v-col cols="12" sm="6" md="3">
      <v-text-field label="Username" outlined class="sr-only form-control" 
      v-model="user.username" 
      type="text" 
      id="username" 
      placeholder="Username" 
      required 
      autofocus
      >
      </v-text-field>
      </v-col>
       <v-col cols="12" sm="6" md="3">
      <v-text-field label="Password" outlined class="sr-only form-control" 
      v-model="user.password" 
      type="password" 
      id="password" 
      placeholder="Password" 
      required
      >
      </v-text-field>
       </v-col>
      </v-row>
      <router-link :to="{ name: 'register' }">Need an account? </router-link>
      <button type="submit" class="secondary--text">| Sign in</button>
     </v-container>
    </v-form>


  </div>
</template>

<script>

import authService from "../services/AuthService";


export default {
  name: "login",

  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
  
};

</script>
