<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-row>
        <v-col>
          <h1>Inventory monitor</h1>
          <p>Demonstrates actual brand's information</p>

          <v-data-table
            :headers="headers"
            :items="forecasts"
            hide-default-footer
            :loading="loading"
            class="elevation-1"
          >
            <v-progress-linear v-slot:progress color="blue" indeterminate></v-progress-linear>
          </v-data-table>
        </v-col>
      </v-row>
    </v-slide-y-transition>

    <v-alert :value="showError" type="error" v-text="errorMessage">Server api doesn't response</v-alert>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { SumOfInventory } from "../models/SumOfInventory";
import axios from "axios";

@Component({})
export default class InventoryDisplayView extends Vue {
  private loading: boolean = true;
  private showError: boolean = false;
  private errorMessage: string = "Error while loading inventory:.";
  private forecasts: SumOfInventory[] = [];
  private headers = [
    { text: "Name", value: "brandName" },
    { text: "Count", value: "quantity" }
  ];

  private async created() {
    await this.fetchSumOfInventory();
    this.intervalFetchData();
  }
  private intervalFetchData() {
    setInterval(() => {
      this.fetchSumOfInventory();
    }, 1000);
  }
  private async fetchSumOfInventory() {
    try {
      const response = await axios.get<SumOfInventory[]>("/api/SumOfInventory");
      this.forecasts = response.data;
    } catch (e) {
      this.showError = true;
      this.errorMessage = `Error while loading inventory: ${e.message}.`;
    }
    this.loading = false;
  }
}
</script>
