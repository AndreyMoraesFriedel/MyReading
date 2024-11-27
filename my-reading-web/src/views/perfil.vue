<template>
  <div class="container">
    <div class="perfil-perfil">
      <!--NAV-->
      <header>
        <!--FundoNav-->
        <div> <!--Imagem Cabecalho-->
            <img
              src="../../public/external/header.svg"
              alt="header"
              class="header"
            />
          </div>
        <!--Logo-->
          <div>
            <img
              src="/external/logo.png"
              alt="logo"
              class="logo"
            />
            <span class="name-logo">MyReading</span>
          </div>
        <!--Opcoes-->
          <nav>
            <ul>
              <!--Biblioteca-->
              <li style="list-style-type: none">
                <button  @click="goToBiblioteca" style="cursor: pointer;">
                  <img
                    src="/external/IconedeBiblioteca.png"
                    alt="Icone de Biblioteca"
                    class="nav-icone-de-biblioteca"
                  />
                  <span class="nav-texto-biblioteca">Biblioteca</span>
                </button>
              </li>
              <!--Leitura-->
              <li style="list-style-type: none">
                <button  @click="goToLeitura" style="cursor: pointer;">
                  <img
                    src="/external/IconedeLeitura.png"
                    alt="Icone de Leitura"
                    class="nav-icone-de-leitura"
                  />
                  <span class="nav-texto-leitura">Leitura</span>
                </button>
              </li>
              <!--Perfil-->
              <li style="list-style-type: none">
                <img
                  src="/external/IconedePerfil.png"
                  alt="Icone de Perfil"
                  class="nav-icone-de-perfil"
                />
                <span class="nav-texto-perfil">Perfil</span>
              </li>
            </ul>
          </nav>
        <!--Ofensiva-->
          <div class="streak">
              <img
                src="/external/livrostreak.svg"
                alt="LivroDoStreak"
                class="streak-livro"
              />
              <span class="streak-numero">{{ streakDays }}</span>
          </div>
      </header>
      <main>
        <!-- Foto de Perfil -->
        <div>
          <!-- Círculo -->
          <div class="perfil-ellipse1"></div>
          <!-- Foto -->
          <img
            :src="fotodousuario || '/external/default-profile.png'"
            alt="Foto do Usuário"
            class="perfil-image1"
          />
        </div>
        <!--QuadradoInfomativo-->
        <div class="perfil-quadrado-informativo">
          <img
            src="/external/rectangle102686-2pvv.svg"
            alt="Rectangle102686"
            class="perfil-rectangle101"
          />
          <div> <!--NOME DO USUÁRIO-->
            <img
              src="/external/customer2687-bfv8-200h.png"
              alt="Customer2687"
              class="perfil-customer"
            />
            <span class="perfil-text11">NOME DO USUÁRIO: {{ nomedousuario }}</span>
          </div>
          <div> <!--TEMPO TOTAL DE LEITURA-->
            <img
              src="/external/numbers2687-4gyo-200h.png"
              alt="Numbers2687"
              class="perfil-numbers"
            />
            <span class="perfil-text14">TEMPO TOTAL DE LEITURA: {{ tempototaldeleitura }}</span>
          </div>
          <div>
            <img
              src="/external/schedule2688-16en-200h.png"
              alt="Schedule2688"
              class="perfil-schedule"
            />
            <span class="perfil-text13">TEMPO DE OFENSIVA: {{ streakDays }}</span>
          </div>
          <!--Editar Perfil-->
          <!--<div><img src="/external/pencil2688-onr-200w.png" alt="Pencil2688" class="perfil-pencil"/><span class="perfil-text12">EDITAR PERFIL</span></div> -->
          <!--SAIR-->
          <div style="cursor: pointer;"  @click="sair"> 
            <img
              src="/external/emergencyexit2688-9g5e-200w.png"
              alt="EmergencyExit2688"
              class="perfil-emergency-exit"
            />
            <span class="perfil-text10">SAIR</span>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import axios from '../http-common';

export default {
  name: 'Perfil - MyReading',
  data() {
    return {
      streakDays: 0,
      nomedousuario: ``,
      tempototaldeleitura: 0,
      fotodousuario: ''
    };
  },
  metaInfo: {
    title: 'Perfil',
  },
  created() {
    const userId = localStorage.getItem('userId'); 
    if (userId) {
      this.obterStreakDoUsuario(userId); 
      this.obterInformacoesDoUsuario(userId);      
      this.obterFotoDoUsuario(userId);
    } else {
      console.error('Usuário não autenticado!');
      this.$router.push('/'); 
    }
  },
  methods: {
    obterStreakDoUsuario(userId) {
      axios
        .get(`/api/v1/reading-streak/total/${userId}`)
        .then((response) => {
          this.streakDays = response.data;
          localStorage.setItem('streakDays', this.streakDays);
        })
        .catch((error) => {
          console.error('Erro ao buscar a streak do usuário:', error);
        });
    },
    obterInformacoesDoUsuario(userId) {
      axios
        .get(`/api/v1/user/${userId}`)
        .then((response) => {
          const { name, totalReadingTime} = response.data;
          this.nomedousuario = name;
          this.tempototaldeleitura = totalReadingTime;

          // Armazena as informações no localStorage, caso necessário
          localStorage.setItem('nomedousuario', name);
          localStorage.setItem('tempototaldeleitura', totalReadingTime);
        })
        .catch((error) => {
          console.error('Erro ao buscar informações do usuário:', error);
        });
    },
    obterFotoDoUsuario(userId) {
      axios
        .get(`/api/v1/user/${userId}/download`, {
          responseType: 'blob', // Recebe a foto como Blob
        })
        .then((response) => {
          // Cria uma URL temporária para o Blob recebido
          const fotoUrl = URL.createObjectURL(response.data);
          this.fotodousuario = fotoUrl;
        })
        .catch((error) => {
          console.error('Erro ao buscar a foto do usuário:', error);
        });
    },
    goToBiblioteca() {
      this.$router.push('/biblioteca');
    },
    goToLeitura() {
      this.$router.push('/leitura');
    },
    /*
    goToEditarPerfil() {
      this.$router.push('/editar-perfil');
    },
    */
    sair() {
      // Limpa o localStorage
      localStorage.removeItem('userId');
      localStorage.removeItem('userName');
      localStorage.removeItem('userPhoto');
      localStorage.removeItem('nomedousuario');
      localStorage.removeItem('tempototaldeleitura');

      // Redireciona para o login
      this.$router.push('/');
    }
  }
}
</script>

<style scoped>
.container {
  width: 100%;
  display: flex;
  overflow: auto;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
}
.perfil-perfil {
  width: 100%;
  height: 1080px;
  display: flex;
  overflow: hidden;
  position: relative;
  align-items: flex-start;
  flex-shrink: 0;
  justify-content: flex-end;
  background-color: rgba(255, 197, 109, 1);
}
.header {
  top: 0px;
  left: 0px;
  width: 1920px;
  height: 140px;
  position: absolute;
}
.logo {
  top: 28px;
  left: 51px;
  width: 87px;
  height: 84px;
  position: absolute;
  border-radius: 1px;
}
.name-logo {
  top: 57px;
  left: 173px;
  color: rgba(255, 255, 255, 1);
  width: 229px;
  height: auto;
  position: absolute;
  font-size: 40px;
  font-style: Regular;
  text-align: justified;
  font-family: Prata;
  font-weight: 400;
  line-height: 24px;
  font-stretch: normal;
  text-decoration: none;
}
.nav-icone-de-biblioteca {
  top: 30px;
  left: 1300px;
  width: 73px;
  height: 49px;
  position: absolute;
}
.nav-texto-biblioteca {
  top: 85px;
  left: 1290px;
  color: rgba(255, 255, 255, 1);
  width: 93px;
  height: auto;
  position: absolute;
  font-size: 20px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
.nav-icone-de-leitura {
  top: 30px;
  left: 1422px;
  width: 73px;
  height: 49px;
  position: absolute;
}
.nav-texto-leitura {
  top: 85px;
  left: 1422px;
  color: rgba(232, 232, 232, 1);
  height: auto;
  position: absolute;
  font-size: 20px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
.nav-icone-de-perfil {
  top: 30px;
  left: 1531px;
  width: 73px;
  height: 49px;
  position: absolute;
}

.nav-texto-perfil {
  top: 85px;
  left: 1543px;
  color: rgba(255, 255, 255, 1);
  height: auto;
  position: absolute;
  font-size: 20px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
.streak{
  top: 30px;
  left: 1649px;
  width: 185px;
  height: 65px;
  position: absolute;
  box-sizing: content-box;
  border-color: rgba(0, 0, 0, 1);
  border-style: solid;
  border-width: 10px;
  border-radius: 40px;
}
.streak-numero {
  top: 1px;
  color: rgba(241, 241, 241, 1);
  right: 1px;
  width: 107px;
  height: auto;
  position: absolute;
  font-size: 64px;
  font-style: Regular;
  text-align: center;
  font-family: Jersey 25;
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
.streak-livro {
  top: 6px;
  right: 104px;
  width: 65px;
  height: 57px;
  position: absolute;
}

/****************************************/
.perfil-ellipse1 {
  top: 200px;
  left: 777px;
  width: 348px;
  height: 321px;
  position: absolute;
  box-sizing: content-box;
  border-color: rgba(0, 0, 0, 1);
  border-style: solid;
  border-width: 11px;
  border-radius: var(--dl-radius-radius-radius4);
  border-top-left-radius: var(--dl-radius-radius-round);
  border-top-right-radius: var(--dl-radius-radius-round);
  border-bottom-left-radius: var(--dl-radius-radius-round);
  border-bottom-right-radius: var(--dl-radius-radius-round);
}
.perfil-image1 {
  top: 209px;
  left: 787px;
  width: 350px;
  height: 325px;
  position: absolute;
  border-radius: 50%;
  object-fit: cover;
}

.perfil-rectangle101 {
  top: 721px;
  left: 639px;
  width: 643px;
  height: 280px;
  position: absolute;
  box-sizing: content-box;
  border-radius: 50px;
  border: 10px solid black;
  box-shadow: 5px 10px 15px 0px rgba(0, 0, 0, 0.5);
}
.perfil-text10 {
  top: 949px;
  left: 703px;
  color: rgba(255, 255, 255, 1);
  width: 56px;
  height: auto;
  position: absolute;
  font-size: 24px;
  font-style: Bold;
  text-align: justified;
  font-family: Inter;
  font-weight: 700;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.perfil-text11 {
  top: 751px;
  left: 703px;
  color: rgba(255, 255, 255, 1);
  width: 556px;
  height: auto;
  position: absolute;
  font-size: 24px;
  font-style: Bold;
  text-align: justified;
  font-family: Inter;
  font-weight: 700;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
/*
.perfil-text12 {
  top: 901px;
  left: 702px;
  color: rgba(255, 255, 255, 1);
  width: 196px;
  height: auto;
  position: absolute;
  font-size: 24px;
  font-style: Bold;
  text-align: justified;
  font-family: Inter;
  font-weight: 700;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
  */
.perfil-text13 {
  top: 854.583740234375px;
  left: 698.9999389648438px;
  color: rgba(255, 255, 255, 1);
  width: 360px;
  height: auto;
  position: absolute;
  font-size: 24px;
  font-style: Bold;
  text-align: justified;
  font-family: Inter;
  font-weight: 700;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.perfil-text14 {
  top: 802.67041015625px;
  left: 701.9999389648438px;
  color: rgba(255, 255, 255, 1);
  width: 465px;
  height: auto;
  position: absolute;
  font-size: 24px;
  font-style: Bold;
  text-align: justified;
  font-family: Inter;
  font-weight: 700;
  line-height: normal;
  font-stretch: normal;
  text-decoration: none;
}
.perfil-customer {
  top: 751px;
  left: 667.1082763671875px;
  width: 31px;
  height: 29px;
  position: absolute;
}
.perfil-numbers {
  top: 803px;
  left: 667px;
  width: 31px;
  height: 29px;
  position: absolute;
}


.perfil-schedule {
  top: 855px;
  left: 667px;
  width: 24px;
  height: 24px;
  position: absolute;
}
.perfil-emergency-exit {
  top: 945px;
  left: 667px;
  width: 24px;
  height: 29px;
  position: absolute;
}
/*
.perfil-pencil {
  top: 901px;
  left: 667px;
  width: 25px;
  height: 26px;
  position: absolute;
}
  */
.perfil-quadrado-informativo{
  top: -121px;
  left: 8px;
  position: absolute;
}
.perfil-text10, .perfil-text11, .perfil-text12, .perfil-text13, .perfil-text14 {
  left: 703px; /* Alinhamento dos textos dentro do quadrado */
}
</style>
