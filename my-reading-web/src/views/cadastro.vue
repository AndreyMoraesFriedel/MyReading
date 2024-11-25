<template>
  <div class="cadastro-container">
    <div class="cadastro-cadastro">
      <!--QuadradoInformativo-->
      <img
        src="/external/rectangle72664-l7jm-1100h.png"
        alt="Rectangle72664"
        class="cadastro-rectangle7"
      />
      <span class="cadastro-text3">Cadastre-se</span>
      <!-- Nome -->
      <input
        type="text"
        v-model="usuario.nome"
        placeholder="Digite seu nome"
        class="cadastro-retangulo-do-nome"
      />
      <!-- Email -->
      <input
        type="email"
        v-model="usuario.email"
        placeholder="Digite seu email"
        class="cadastro-retangulo-do-email"
      />
      <!-- Senha -->
      <input
        type="password"
        v-model="usuario.senha"
        placeholder="Digite sua senha"
        class="cadastro-retangulo-da-senha"
      />
      <!-- Confirmar Senha -->
      <input
        type="password"
        v-model="usuario.confirmarSenha"
        placeholder="Confirme sua senha"
        class="cadastro-retangulo-da-confirma-senha"
      />

      <!--FotoPerfil-->
      <div class="cadastro-foto-perfil">
        <label for="cadastro-coverImage" style="color: white">Foto de Perfil:</label>
        <input type="file" @change="selecionarImagem" id="cadastro-coverImage" accept="image/*" />
      </div>

      <!-- Botão Criar Conta com evento de clique -->
      <button class="cadastro-button-criar-conta" @click="registrar">
        <span class="cadastro-text6">Criar Conta</span>
      </button>

      <!--Botao Voltar -->
      <button class="cadastro-button-voltar" @click="voltarLogin">
        <span class="cadastro-text-voltar">Voltar</span>
      </button>
    </div>
  </div>
</template>

<script>
import axios from '../http-common';

export default {
  name: 'Cadastro',
  data() {
    return {
      usuario: {
        nome: '',
        email: '',
        senha: '',
        confirmarSenha: '',
        foto: null,
      },
    };
  },
  metaInfo: {
    title: 'Cadastro',
  },
  methods: {
    async registrar() {
      if (this.usuario.senha !== this.usuario.confirmarSenha) {
        alert('As senhas não coincidem!');
        return;
      }
      if (!this.usuario.foto) {
        alert('Por favor, anexe uma foto!');
        return;
      }

      // Configurar o FormData
      const formData = new FormData();
      formData.append('Name', this.usuario.nome);
      formData.append('Email', this.usuario.email);
      formData.append('Password', this.usuario.senha);
      formData.append('Photo', this.usuario.foto);

      try {
        // Enviar os dados para a API
        const response = await axios.post('/api/v1.0/user', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        });

        const userId = response.data.id;

        await axios.post(`/api/v1/reading-streak`, {
            userId: userId,
          });

        alert('Usuário cadastrado com sucesso!');
        this.$router.push('/'); // Redirecionar após o cadastro
      } catch (error) {
        console.error('Erro ao cadastrar usuário:', error);
        alert('Erro ao cadastrar usuário. Verifique os dados e tente novamente.');
      }
    },
    selecionarImagem(event) {
      this.usuario.foto = event.target.files[0];
    },
    voltarLogin() {
      this.$router.push('/');
    },
  },
};
</script>

<style scoped>
.cadastro-container {
  width: 100%;
  display: flex;
  overflow: auto;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
  background-color: rgb(129, 116, 116);
}
.cadastro-cadastro {
  width: 100%;
  height: 1080px;
  display: flex;
  overflow: hidden;
  position: relative;
  align-items: flex-start;
  flex-shrink: 0;
  background-color: rgba(70, 67, 67, 0.35);
}
.cadastro-rectangle7 {
  top: 32px;
  left: 445px;
  width: 1091px;
  height: 1006px;
  position: absolute;
  border-radius: 40px;
}
.cadastro-retangulo-do-nome {
  top: 160px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-do-email {
  top: 320px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-da-senha {
  top: 480px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-da-confirma-senha {
  top: 640px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-do-nome,
.cadastro-retangulo-do-email,
.cadastro-retangulo-da-senha,
.cadastro-retangulo-da-confirma-senha
{
  background-color: #FFC56D;
  color: rgb(0, 0, 0);
  display: block;
  padding: 10px;
  margin: 10px 0;
  font-size: 30px; 
}
.cadastro-text1 {
  top: 436px;
  left: 660px;
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
.cadastro-text2 {
  top: 264px;
  left: 660px;
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
.cadastro-text3 {
  top: 64px;
  left: 802px;
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
.cadastro-text4 {
  top: 606px;
  left: 660px;
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
.cadastro-text5 {
  top: 766px;
  left: 660px;
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
.cadastro-button-voltar {
  gap: 8px;
  top: 890px;
  left: 643px;
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

.cadastro-button-voltar:hover {
  background-color: rgba(255, 180, 90, 1); 
}

.cadastro-button-voltar:active {
  transform: scale(0.98);
}

.cadastro-text-voltar {
  color: rgba(30, 30, 30, 1);
  font-size: 32px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  text-decoration: none;
}
.cadastro-button-criar-conta {
  gap: 8px;
  top: 890px;
  left: 1020px;
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

.cadastro-button-criar-conta:hover {
  background-color: rgba(255, 180, 90, 1); /* Cor mais escura ao passar o mouse */
}

.cadastro-button-criar-conta:active {
  transform: scale(0.98); /* Efeito de "pressão" no clique */
}

.cadastro-text6 {
  color: rgba(30, 30, 30, 1);
  font-size: 32px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  text-decoration: none;
}

input[type="file"] {
    border: 1px solid #ccc;
    border-radius: 8px;
    margin: 8px;
    width: 500px;
}
.cadastro-foto-perfil{
  left: 630px;
  top: 800px;
  position: absolute;
  font-size: 30px;
}
</style>
