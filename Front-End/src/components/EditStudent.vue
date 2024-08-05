<template>
    <div>
        <StudentForm :student="student" @submit="editStudent" />
    </div>
</template>
  
<script>
  import StudentForm from './StudentForm.vue';
  import axios from 'axios';
  
  export default {
    components: { StudentForm },
    data() {
      return {
        student: { studentID: '', name: '', cpf: '', email: '' }
      }
    },
    methods: {
      fetchStudent() {
        axios.get(`https://localhost:7280/api/students/${this.$route.params.id}`)
          .then(response => {
            this.student = response.data
            
          });
      },
      editStudent(updatedStudent) {
        if (updatedStudent?.name && updatedStudent?.email) {
            axios.put(`https://localhost:7280/api/students/${this.$route.params.id}`, updatedStudent)
            .then(() => {
                this.$swal({ icon: 'success', title: 'Sucesso!', text: 'Aluno atualizado com sucesso', confirmButtonColor: "#2196F3" })
                    .then((res) => this.$router.push({ name: 'StudentList' }))
                
            });
        }
      }
    },
    created() {
      this.fetchStudent();
    }
  };
</script>
  