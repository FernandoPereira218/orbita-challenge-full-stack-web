<template>
    <v-card class="mx-auto" width="80%" style="margin-top: 25px;" :title="!!student.studentID === false ? 'Adicionar Aluno' : 'Editar Aluno'">
        <v-sheet class="mx-auto" width="50%">
            <v-form fast-fail @submit.prevent="handleSubmit" class="form-data" v-model="isFormValid"> 
                <v-text-field
                    disabled
                    v-model="student.studentID"
                    autocomplete="off"
                    label="RA"
                ></v-text-field>
                
                <v-text-field
                    v-model="student.name"
                    autocomplete="off"
                    label="Nome"
                    :rules="[rules.required, rules.name]"
                ></v-text-field>

                <v-text-field
                    v-model="student.email"
                    type="email"
                    label="Email"
                    autocomplete="off"
                    :rules="[rules.required, rules.email]"
                ></v-text-field>

                <v-text-field
                    v-model="student.cpf"
                    autocomplete="off"
                    label="CPF"
                    :disabled="!!student.studentID"
                    :rules="[rules.required, rules.cpf]"
                ></v-text-field>
               
                <v-btn class="mt-2" type="submit" block color="info" :disabled="!isFormValid">Salvar</v-btn>
                <v-btn class="mt-2" type="button" variant="outlined" block @click="this.$router.push({ name: 'StudentList' })">Cancelar</v-btn> 
            </v-form>
        </v-sheet>
    </v-card>
</template>

<script>
const rules = {
    required: value => !!value || 'Campo obrigatório',
    cpf: value => {
        const pattern = /(\d{3})(\d{3})(\d{3})(\d{2})/
        return (pattern.test(value) && value.length === 11) || 'CPF inválido.'
    },
    email: value => {
        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        return pattern.test(value) || 'Email inválido.'
    },
    name: value => value.length >= 3 || 'Mínimo de 3 caracteres'
}
export default {
  props: ['student'],
  data() {
    return this.student ? {...this.student, rules: rules, isFormValid: false, } : {
      name: '',
      email: '',
      cpf: '',
      studentID: '',
      rules: rules,
      isFormValid: false,
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