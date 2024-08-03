<template>
  <v-app>
    <v-main>
        <router-view />
        <v-navigation-drawer >
            <v-list-item title="Grupo A"></v-list-item>
            <v-divider></v-divider>
            <v-list-item link title="Alunos"></v-list-item>
        </v-navigation-drawer>
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
  //
</script>
