<template>
    <v-card class="mx-auto" width="80%" style="margin-top: 25px;">
        <v-sheet class="mx-auto" width="50%">
            <v-form fast-fail @submit.prevent="handleSubmit" class="form-data"> 
                <v-text-field
                    disabled
                    v-model="student.studentID"
                    
                    label="RA"
                ></v-text-field>
                
                <v-text-field
                    v-model="student.name"
                    
                    label="Nome"
                    required
                ></v-text-field>

                <v-text-field
                    v-model="student.email"
                    type="email"
                    label="Email"
                    required
                ></v-text-field>

                <v-text-field
                    v-model="student.cpf"
                    
                    label="CPF"
                    required
                ></v-text-field>
               
                <v-btn class="mt-2" type="submit" block color="info">Salvar</v-btn>
                <v-btn class="mt-2" type="button" variant="outlined" block @click="this.$router.push({ name: 'StudentList' })">Voltar</v-btn> 
            </v-form>
        </v-sheet>
    </v-card>
</template>

<script>
export default {
  props: ['student'],
  data() {
    return this.student ? {...this.student} : {
      name: '',
      email: '',
      cpf: '',
      studentID: '',
    };
  },
  methods: {
    handleSubmit() {
        const data = {...this.student}
      this.$emit('submit', data);
    }
  }
};
</script>

<style>
.form-data {
    margin: 15px;
}
</style>