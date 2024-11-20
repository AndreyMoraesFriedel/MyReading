<template>
  <div class="telade-login-container">
    <div class="telade-login-fundo">
      <img
        src="/external/FundoLaranja.png"
        alt="FundoLaranja"
        class="telade-login-fundo-laranja"
      />
      <img
        src="/external/FundoPreto.png"
        alt="FundoPreto"
        class="telade-login-fundo-preto"
      />
    </div>
    <span class="telade-login-text1">MyReading</span>
    <span class="telade-login-text2">Bem vindo ao</span>
    <span class="telade-login-text4">Faça seu Login</span>
    <!-- Campo de entrada para email -->
    <input
      type="email"
      v-model="email"
      placeholder="Digite seu email"
      class="telade-login-retangulo-do-email"
    />
    <!-- Campo de entrada para senha -->
    <input
      type="password"
      v-model="senha"
      placeholder="Digite sua senha"
      class="telade-login-retangulo-da-senha"
    />
    <span class="telade-login-text6">
      Auxílio adequado para atingir o seu objetivo através de um sistema
      interativo satisfatório e motivador.
    </span>
    <!-- Botão "Entrar" com evento de clique -->
    <button class="telade-login-button1" @click="logar()">
      <span class="telade-login-text7">Entrar</span>
    </button>
    <button class="telade-login-button2" @click="goToCadastro()">
      <span class="telade-login-text8">Cadastre-se</span>
    </button>
    <img
      src="/external/circulodologo396-bdlh.svg"
      alt="CirculoDoLogo"
      class="telade-login-circulo-do-logo"
    />
    <img
      src="/external/logo.png"
      alt="Logo"
      class="telade-login-logo"
    />
  </div>
</template>

<script>
import axios from '../http-common';

export default {
  name: 'TeladeLogin',
  data() {
    return {
      email: '', // Armazena o valor do email
      senha: '', // Armazena o valor da senha
      errorMessage: '', // Mensagem de erro
    };
  },
  methods: {
    async logar() {
      try {
        // Verifica se os campos de email e senha estão preenchidos
        if (!this.email || !this.senha) {
          this.errorMessage = 'Preencha todos os campos.';
          alert(this.errorMessage);
          return;
        }

        // Realiza a requisição POST para o endpoint de login da API
        const response = await axios.post('/api/v1.0/user/login', {
          email: this.email,
          password: this.senha,
        });

        const user = response.data;

        // Redireciona para a biblioteca após login bem-sucedido
        this.$router.push({
          path: '/biblioteca',
          query: { id: user.id, name: user.name, photo: user.photo },
        });
      } catch (error) {
        // Exibe mensagem caso o usuário não seja encontrado ou ocorra um erro na API
        if (error.response && error.response.status === 401) {
          this.errorMessage = 'Email ou senha inválidos.';
        } else {
          this.errorMessage = 'Ocorreu um erro ao fazer login.';
        }
        alert(this.errorMessage);
      }
    },
    goToCadastro() {
      this.$router.push('/cadastro');
    },
  },
  metaInfo: {
    title: 'Login',
  },
};
</script>



<style scoped>
.telade-login-container {
  width: 100%;
  height:100%;
  display: flex;
  overflow: hidden;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
}

.telade-login-fundo {
  top: 0px;
  left: 0px;
  width: 1920px;
  height: 1080px;
  display: flex;
  position: absolute;
  align-items: flex-start;
  flex-shrink: 0;
}
.telade-login-fundo-laranja {
  top: 0px;
  left: 0px;
  width: 843px;
  height: 1080px;
  position: absolute;
}
.telade-login-fundo-preto {
  top: 0px;
  left: 813px;
  width: 1133px;
  height: 1080px;
  position: absolute;
  border-radius: 40px;
}
.telade-login-text1 {
  top: 500px;
  left: 267px;
  color: rgba(0, 0, 0, 1);
  height: auto;
  position: absolute;
  font-size: 48px;
  font-style: Regular;
  text-align: left;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-text2 {
  top: 130px;
  left: 240px;
  color: rgba(0, 0, 0, 1);
  height: auto;
  position: absolute;
  font-size: 48px;
  font-style: Regular;
  text-align: left;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-retangulo-do-email {
  top: 375px;
  left: 1000px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.telade-login-retangulo-da-senha {
  top: 589px;
  left: 1000px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.telade-login-retangulo-do-email, .telade-login-retangulo-da-senha {
  background-color: #FFC56D;
  color: rgb(0, 0, 0);
  display: block;
  padding: 10px;
  margin: 10px 0;
  font-size: 30px; 
}
.telade-login-text3 {
  top: 418px;
  left: 1050px;
  color: rgba(0, 0, 0, 1);
  height: auto;
  position: absolute;
  font-size: 32px;
  font-style: Regular;
  text-align: left;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-text4 {
  top: 198px;
  left: 1152px;
  color: rgba(255, 197, 109, 1);
  height: auto;
  position: absolute;
  font-size: 64px;
  font-style: Regular;
  text-align: left;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-text5 {
  top: 632px;
  left: 1050px;
  color: rgba(0, 0, 0, 1);
  height: auto;
  position: absolute;
  font-size: 32px;
  font-style: Regular;
  text-align: left;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-text6 {
  top: 640px;
  left: 89px;
  color: rgba(0, 0, 0, 1);
  width: 610px;
  height: auto;
  position: absolute;
  font-size: 36px;
  font-style: Regular;
  text-align: center;
  font-family: Inter;
  font-weight: 400;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.telade-login-button1 {
  gap: 8px;
  top: 846px;
  left: 1221px;
  width: 318px;
  height: 110px;
  display: flex;
  padding: 12px;
  position: absolute;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
  align-items: center;
  flex-shrink: 0;
  border: 1px solid rgba(151, 81, 1, 1);
  border-radius: 8px;
  justify-content: center;
  background-color: rgba(255, 197, 109, 1);
  transition: background-color 0.3s, transform 0.1s;
  cursor: pointer;
}

.telade-login-button1:hover {
  background-color: rgba(255, 180, 90, 1); /* Cor mais escura no hover */
}

.telade-login-button1:active {
  transform: scale(0.98); /* Efeito de "pressão" no clique */
}

.telade-login-text7 {
  color: rgba(30, 30, 30, 1);
  font-size: 32px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  text-decoration: none;
}

.telade-login-button2 {
  gap: 8px;
  top: 846px;
  left: 262px;
  width: 318px;
  height: 110px;
  display: flex;
  padding: 12px;
  position: absolute;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
  align-items: center;
  flex-shrink: 0;
  border: 1px solid rgba(151, 81, 1, 1);
  border-radius: 8px;
  justify-content: center;
  background-color: rgba(70, 68, 68, 1);
  transition: background-color 0.3s, transform 0.1s;
  cursor: pointer;
}

.telade-login-button2:hover {
  background-color: rgb(43, 43, 43); /* Cor mais clara no hover */
}

.telade-login-button2:active {
  transform: scale(0.98); /* Efeito de "pressão" no clique */
}

.telade-login-text8 {
  color: rgba(255, 197, 109, 1);
  font-size: 32px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  text-decoration: none;
}
.telade-login-circulo-do-logo {
  top: 244px;
  left: 288px;
  width: 212px;
  height: 201px;
  position: absolute;
}
.telade-login-logo {
  top: 273px;
  left: 319px;
  width: 150px;
  height: 150px;
  position: absolute;
}
</style>
