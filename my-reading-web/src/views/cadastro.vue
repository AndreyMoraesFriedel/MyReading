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
      <!--Nome-->
      <input
        type="text"
        v-model="Text"
        placeholder="Digite seu nome"
        class="cadastro-retangulo-do-nome"
      />
      <!--Email-->
      <input
        type="email"
        v-model="email"
        placeholder="Digite seu email"
        class="cadastro-retangulo-do-email"
      />
      <!--Senha-->
      <input
        type="password"
        v-model="senha"
        placeholder="Digite sua senha"
        class="cadastro-retangulo-da-senha"
      />
      <!--CSenha-->
      <input
        type="password"
        v-model="senha"
        placeholder="Confirme sua senha"
        class="cadastro-retangulo-da-confirma-senha"
      />
      
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
export default {
  name: 'Cadastro',
  metaInfo: {
    title: 'Cadastro',
  },
  methods: {
    registrar() {
      // Aqui você pode adicionar lógica de verificação e salvamento dos dados
      
      // Redireciona para a tela de login após o cadastro
      this.$router.push('/');
    },
    voltarLogin() {
      this.$router.push('/');
    }
  }
}
</script>

<style scoped>
.cadastro-container {
  width: 100%;
  display: flex;
  overflow: auto;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
  background-color: black;
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
  top: 221px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-do-email {
  top: 392px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-da-senha {
  top: 563px;
  left: 610px;
  width: 761px;
  height: 124px;
  position: absolute;
  border-radius: 15px;
}
.cadastro-retangulo-da-confirma-senha {
  top: 723px;
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
  top: 97px;
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
</style>
