import Vue from 'vue';
import Router from 'vue-router';
import CreateBrandForm from '@/views/CreateBrandForm.vue';
import InventotyDisplay from '@/views/InventotyDisplay.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'createBrandForm',
      component: CreateBrandForm,
    },
    {
      path: '/fetch-data',
      name: 'fetch-data',
      component: InventotyDisplay,
    },
  ],
});
