<template>
    <div>
        <h1>Cadastrar Aluno</h1>
        <StudentForm :student="student" @submit="addNewStudent" />
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
        addNewStudent(addedStudent) {
        if (addedStudent?.name && addedStudent?.email && addedStudent?.cpf) {
            axios.post('https://localhost:7280/api/students', addedStudent)
            .then(() => {
                this.$swal({ icon: 'success', title: 'Sucesso!', text: 'Aluno adicionado com sucesso', confirmButtonColor: "#2196F3" })
                    .then((res) => this.$router.push({ name: 'StudentList' }))
                });
        }
      }
    }
  };
</script>
  