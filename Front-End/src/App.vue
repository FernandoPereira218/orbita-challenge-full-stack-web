<template>
  <v-app>
    <v-main>
        <router-view />
        <v-navigation-drawer>
            <v-list-item @click="$router.push({ name: 'StarterPage' })" max-height="80px"><v-img src="https://maisaedu.com.br/hubfs/site-grupo-a/logo-mais-a-educacao.svg" max-height="75px"></v-img></v-list-item>
            <v-divider></v-divider>
            <v-list-item link title="Alunos" @click="$router.push({ name: 'StudentList' })"></v-list-item>
        </v-navigation-drawer>
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
  //
</script>
