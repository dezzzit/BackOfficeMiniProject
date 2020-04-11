<template>
  <v-container fluid>
    <div id="app">
      <v-app id="inspire">
        <v-row align="center">
          <v-form ref="form" v-model="valid" :lazy-validation="lazy">
            <v-alert
              :value="showSuccess"
              type="success"
            >
              New brand was added
            </v-alert>
            <v-text-field v-model="name" :counter="15" :rules="nameRules" label="Name" required></v-text-field>

            <v-btn :disabled="!valid" color="success" class="mr-4" @click="create">Create</v-btn>

            <v-btn color="error" class="mr-4" @click="reset">Reset Form</v-btn>
             <br/>
            
          </v-form>
        </v-row>
      </v-app>
    </div>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src
import axios from 'axios';
import {Brand} from '../models/Brand';
//import Vuetify from 'vuetify';

// @Component({
//   components: {
//     HelloWorld,
//   },
// })
// export default class Home extends Vue {
// }

export default Vue.extend({
  el: '#app',
  data: () => ({
    showSuccess: false,
    valid: true,
    name: '',
    nameRules: [
      (v: string) => !!v || 'Name is required',
      (v: string) => (v && v.length <= 10) || 'Name must be less than 10 characters',
    ]
  }),

  methods: {
    create () {
      (this.$refs.form as any).validate();
      this.showSuccess = false;
      const headers = {
        'Content-Type': 'application/json', 
        Accept: 'application/json'
      };
      axios.post('api/Brands', JSON.stringify(new Brand(0,this.name)),{headers: headers} )
        .then((response) => {
          console.log(response);
          this.showSuccess = true;
        })
        .catch((error) => {
          console.log(error);
        })
      },
    reset () {
      (this.$refs.form as any).reset()
    },
    resetValidation () {
      (this.$refs.form as any).resetValidation()
    },
  },
})


</script>
