<template>
    <v-container class="fill-height">
      <v-responsive
        class="align-centerfill-height mx-auto"
        max-width="900"
      >
        <v-img
          class="mb-4"
          height="150"
          src="https://maisaedu.com.br/hubfs/site-grupo-a/logo-mais-a-educacao.svg"
        />
  
        <div class="text-center">
  
          <h1 class="text-h2 font-weight-bold">Módulo Acadêmico</h1>
        </div>
  
        <div class="py-4" />
  
        <v-row>
          <v-col cols="12">
            <v-card
              class="py-4"
              color="surface-variant"
              prepend-icon="mdi-account-school"
              rounded="lg"
              variant="outlined"
              @click="$router.push({ name: 'StudentList' })"
            >
  
              <template #title>
                <h2 class="text-h5 font-weight-bold">Alunos</h2>
              </template>
  
              <v-overlay
                opacity=".12"
                scrim="primary"
                contained
                model-value
                persistent
              />
            </v-card>
          </v-col>
        </v-row>
      </v-responsive>
    </v-container>
  </template>
  
  <script setup lang="ts">
    //
  </script>
  