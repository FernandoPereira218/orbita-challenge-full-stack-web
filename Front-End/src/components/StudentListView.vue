<template>
    <v-row no-gutters>
        <v-col>
            <v-text-field v-model="search" class="ma-2 pa-2" density="compact" placeholder="Pesquisar..." hide-details />
            <v-spacer></v-spacer>
        </v-col>
        <v-col style="margin-top: 10px;">
            <v-btn class="ma-2 pa-2" color="success" prepend-icon="mdi-plus" @click="this.$router.push({ name: 'AddStudent' })">Cadastrar Aluno</v-btn>
        </v-col>
    </v-row>
    <v-data-table-server
      v-model:items-per-page="itemsPerPage"
      :headers="headers"
      :items="students"
      :items-length="totalItems"
      :loading="loading"
      :search="search"
      item-value="name"
      @update:options="loadItems"
    >
        <template v-slot:item.action="{ item }">
        <v-tooltip location="top">
            <template v-slot:activator="{ props }">
                <v-btn v-bind="props" color="blue" class="me-2"  icon="mdi-pencil" @click="this.$router.push({ name: 'EditStudent', params: { id: item.studentID } })">
                </v-btn>
            </template>
            <span>Editar</span>
        </v-tooltip>
        <v-tooltip location="top">
            <template v-slot:activator="{ props }">
                <v-btn v-bind="props" color="red" class="me-2" icon="mdi-delete" @click="deleteStudent(item.studentID)">
                </v-btn>
            </template>
            <span>Excluir</span>
        </v-tooltip>

        </template>
    </v-data-table-server>
  </template>
  <script>
    import axios from 'axios'

    const GetData = {
        async getData ({ page, itemsPerPage, search }) {
            return await axios
            .get(`https://localhost:7280/api/students?page=${page}&pageLength=${itemsPerPage}&search=${search}`)
            .then(response => {
                return { items: response.data.list, total: response.data.length }
            })
            .catch(error => {
                console.log(error)
                this.errored = true
                return { items: [], total: 0 }
            })
            .finally(() => {
                this.loading = false
                return { items: [], total: 0 }
            })
        }
    }
  
    export default {
      data: () => ({
        itemsPerPage: 10,
        headers: [
          {
            title: 'Registro Acadêmico',
            align: 'start',
            sortable: false,
            key: 'studentID',
          },
          { title: 'CPF', key: 'cpf', align: 'start' },
          { title: 'Nome', key: 'name', align: 'start' },
          { title: 'Email', key: 'email', align: 'start' },
          { title: 'Ações', value: 'action', align: 'center' },
        ],
        search: '',
        students: [],
        loading: true,
        totalItems: 0,
      }),
      methods: {
        loadItems ({ page, itemsPerPage, search }) {
            this.loading = true
            GetData.getData({ page, itemsPerPage, search }).then(({ items, total }) => {
                this.students = items
                this.totalItems = total
                this.loading = false
            })
        },
        deleteStudent (item) {
            this.$swal(
                {
                    title: 'Atenção',
                    icon: 'warning',
                    text: `Tem certeza que deseja remover este aluno?`,
                    showDenyButton: true,
                    denyButtonText: 'Não',
                    confirmButtonText: 'Sim',
                    confirmButtonColor: '#F44336',
                    denyButtonColor: '#2196F3'
                }).then(result => {
                    if (result.isConfirmed) {
                        axios.delete(`https://localhost:7280/api/students/${item}`)
                        .then(response => {

                            this.$swal({ icon: 'success', title: 'Sucesso!', text: 'Aluno removido com sucesso', confirmButtonColor: "#2196F3" })
                            .then((res) => location.reload())
                        })
                        .catch(err => {
                            this.$swal({ icon: 'error', title: 'Erro!', text: 'Ocorreu um erro', confirmButtonColor: "#2196F3" })
                        })
                    }
                });
        }
      },
    }
  </script>