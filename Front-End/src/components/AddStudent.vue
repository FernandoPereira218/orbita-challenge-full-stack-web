<template>
    <div>
      <h1>Adicionar Aluno</h1>
      <StudentForm :student="student" @submit="addStudent" />
    </div>
  </template>
  
  <script>
  import StudentForm from './StudentForm.vue';
  import axios from 'axios';
  
  export default {
    components: { StudentForm },
    data() {
      return {
        student: { studentID: '', name: '', email: '', cpf: '' }
      }
    },
    methods: {
      addStudent(student) {
        console.log('submitStudent', student)
        axios.post('https://localhost:7280/api/students', student)
          .then(() => {
            this.$swal({ icon: 'success', title: 'Sucesso!', text: 'Aluno adicionado com sucesso', confirmButtonColor: "#2196F3" })
                .then((res) => this.$router.push({ name: 'StudentList' }))
          });
      }
    }
  };
  </script>
  