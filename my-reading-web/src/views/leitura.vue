<template>
  <div class="leitura-container">
    <div class="leitura-leitura">
      <!-- NAV -->
      <header>
        <!-- FundoNav -->
        <div>
          <!-- Imagem Cabecalho -->
          <img src="../../public/external/header.svg" alt="header" class="header" />
        </div>
        <!-- Logo -->
        <div>
          <img src="/external/logo.png" alt="logo" class="logo" />
          <span class="name-logo">MyReading</span>
        </div>
        <!-- Opcoes -->
        <nav>
          <ul>
            <!-- Biblioteca -->
            <li style="list-style-type: none">
              <button @click="goToBiblioteca" style="cursor: pointer;">
                <img src="/external/IconedeBiblioteca.png" alt="Icone de Biblioteca" class="nav-icone-de-biblioteca" />
                <span class="nav-texto-biblioteca">Biblioteca</span>
              </button>
            </li>
            <!-- Leitura -->
            <li style="list-style-type: none">
              <img src="/external/IconedeLeitura.png" alt="Icone de Leitura" class="nav-icone-de-leitura" />
              <span class="nav-texto-leitura">Leitura</span>
            </li>
            <!-- Perfil -->
            <li style="list-style-type: none">
              <button  @click="goToPerfil" style="cursor: pointer;">
                <img src="/external/IconedePerfil.png" alt="Icone de Perfil" class="nav-icone-de-perfil" />
                <span class="nav-texto-perfil">Perfil</span>
              </button>
            </li>
          </ul>
        </nav>
        <!-- Ofensiva -->
        <div class="streak">
          <img src="/external/livrostreak.svg" alt="LivroDoStreak" class="streak-livro" />
          <span class="streak-numero">{{ streakDays }}</span>
        </div>
      </header>
      <!-- PARTE DEBAIXO -->
      <main>
        <img src="/external/rectangle68213-lu6-700w.png" alt="Rectangle68213" class="leitura1-rectangle6" />

        <!-- SELEÇÃO DE LIVROS -->
        <select v-model="livroSelecionado" class="leitura1-select">
          <option disabled value="">Selecionar livro</option>
          <option v-for="livro in livrosDoUsuario" :key="livro.id" :value="livro.id">{{ livro.title }}</option>
        </select>

        <!-- Exibe a capa do livro selecionado -->
        <img :src="livrosDoUsuario.find(l => l.id === livroSelecionado)?.capaUrl" class="leitura1-frame3"/>

        <div class="leitura-timer">
          <div id="timer">{{ formattedTime }}</div>
          <div id="status">{{ timerStatus }}</div>
          <button id="startBtn" @click="startTimer">Iniciar</button>
          <button id="pauseBtn" @click="togglePause">{{ isPaused ? "Despausar" : "Pausar" }}</button>
          <button id="finalizeBtn" @click="finalizeTimer">Finalizar</button>
        </div>

        <span class="leitura1-text19">{{ fraseMotivacional }}</span>
        <img src="/external/vector2614-od1c.svg" alt="Vector2614" class="leitura1-vector" />
      </main>
    </div>
  </div>
</template>

<script>
import axios from '../http-common';

export default {
  name: "Leitura",
  data() {
    return {
      livrosDoUsuario: [],
      livro: {
        nome: '',
        autor: '',
        paginas: '',
        capa: null,
      }, 
      livroSelecionado: "", 
      fraseMotivacional: "Frase Motivadora",
      timerInterval: null,
      seconds: 0,
      isPaused: false,
      timerStatus: "Timer zerado",
      streakDays: 0, 
    };
  },
  metaInfo: {
    title: 'Leitura',
  },
  created() {
    const userId = localStorage.getItem('userId');
    this.gerarFraseMotivadora();
    // Se um id do usuário está disponível, busca a streak do backend
    if (userId) {
      this.obterStreakDoUsuario(userId);
      this.carregarLivrosDoUsuario(userId);
    }
  },
  computed: {
    formattedTime() {
      const hrs = String(Math.floor(this.seconds / 3600)).padStart(2, "0");
      const mins = String(Math.floor((this.seconds % 3600) / 60)).padStart(2, "0");
      const secs = String(this.seconds % 60).padStart(2, "0");
      return `${hrs}:${mins}:${secs}`;
    },
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
    async carregarLivrosDoUsuario(userId) {
      try {
        const response = await axios.get(`/api/v1/user/${userId}/books`);
        const livros = response.data;

        // Obtem as URLs de capa para cada livro
        this.livrosDoUsuario = await Promise.all(
          livros.map(async (livro) => {
            const capaUrl = await this.obterUrlCapa(livro.id);
            return {
              ...livro,
              capaUrl,
            };
          })
        );
      } catch (error) {
        console.error('Erro ao carregar livros do usuário:', error);
      }
    },
    async obterUrlCapa(livroId) {
      try {
        const response = await axios.get(`/api/v1/book/${livroId}/download`, {
          responseType: 'blob',
        });
        return URL.createObjectURL(response.data);
      } catch (error) {
        console.error('Erro ao buscar a capa do livro:', error);
        return null; 
      }
    },
    gerarFraseMotivadora() {
      const frases = [
      `"A leitura é para a mente o que o exercício é para o corpo." — Joseph Addison`,
      `"Um livro é um sonho que você segura em suas mãos." — Neil Gaiman`,
      `"Ler é viajar sem sair do lugar." — Cecília Meireles`,
      `"A leitura engrandece a alma." — Voltaire`,
      `"Um leitor vive mil vidas antes de morrer." — George R.R. Martin`,
      `"A leitura de um bom livro é um diálogo incessante: o livro fala e a alma responde." — André Maurois`,
      `"Cada livro é um mundo à espera de ser descoberto." — Alberto Manguel`,
      `"Livros são amigos silenciosos e fiéis." — Confúcio`,
      `"A leitura nos dá um lugar para ir quando temos que ficar onde estamos." — Mason Cooley`,
      `"O livro é um mestre que fala, mas não responde." — Platão`,
      ];
      this.fraseMotivacional = frases[Math.floor(Math.random() * frases.length)];
    },
    startTimer() {
      if (this.livroSelecionado) {
        if (!this.timerInterval) {
          this.timerInterval = setInterval(this.updateTimer, 1000);
          this.timerStatus = "Em andamento";
          this.isPaused = false;
        }
      } else {
        alert("Por favor, selecione um livro.");
      }
    },
    updateTimer() {
      this.seconds++;
    },
    togglePause() {
      if (this.timerInterval) {
        clearInterval(this.timerInterval);
        this.timerInterval = null;
        this.isPaused = true;
        this.timerStatus = "Timer pausado";
      } else if (this.isPaused) {
        this.startTimer();
      }
    },
    finalizeTimer() {
      if (this.timerInterval) {
        clearInterval(this.timerInterval);
        this.timerInterval = null;
      }
      alert(`Timer finalizado. Tempo total: ${this.formattedTime}`);

      const userId = localStorage.getItem('userId');

      if (this.livroSelecionado && userId) {
        this.acrescentarTempoNoLivro(userId, this.livroSelecionado, this.formattedTime); 
        this.acrescentarTempoTotal(userId, this.formattedTime); 
        this.updateStreak(userId);
      } else {
        console.error("Erro: Livro ou usuário não selecionado.");
      }

      // Reseta o timer
      this.timerStatus = "Timer zerado";
      this.seconds = 0;
    },
    async updateStreak(userId) {
      try {
        //Buscando informações da streak do usuário
        const streakResponse = await axios.get(`/api/v1/reading-streak/${userId}`);
        const streakData = streakResponse.data;

        //Verificar a última data atualizada
        const lastStreakDate = new Date(streakData.lastStreak); 
        const today = new Date();
        today.setHours(0, 0, 0, 0); // Zera as horas para comparar apenas as datas

        //Se a última atualização não for hoje, incrementar a streak
        if (lastStreakDate < today) {
          const updateResponse = await axios.put(`/api/v1/reading-streak/${userId}/length-in-days`);
          const updatedData = updateResponse.data;
          this.streakDays = updatedData.lengthInDays;
          console.log(`Streak atualizada! Total de dias: ${updatedData.lengthInDays}`)
        } else {
          console.log('Sua streak já está atualizada para hoje!');
        }
      } catch (error) {
        console.error('Erro ao atualizar a streak:', error);
        console.log('Não foi possível atualizar a streak. Tente novamente.');
      }
    },
    async acrescentarTempoNoLivro(userId, bookId, timeToAdd) {
      try {
        const formattedTime = this.formattedTime; 

        await axios.put(
          `/api/v1/reading-progress/${userId}/book/${bookId}/increment-time`,
          null, 
          { params: { timeToAddInSeconds: formattedTime } } 
        );
        console.log(`Tempo (${timeToAdd} minutos) adicionado ao progresso do livro.`);
      } catch (error) {
        console.error("Erro ao atualizar tempo do livro:", error);
      }
    },
    async acrescentarTempoTotal(userId) {
      try {
        const formattedTime = this.formattedTime; 
        
        await axios.put(
          `/api/v1/user/${userId}/add-reading-time`,
          null, 
          { params: { timeToAddInSeconds: formattedTime } } 
        );
        console.log(`Tempo total de leitura atualizado com sucesso.`);
      } catch (error) {
        console.error("Erro ao atualizar tempo total do usuário:", error);
      }
    },
    goToBiblioteca() {
      this.$router.push("/biblioteca");
    },
    goToPerfil() {
      this.$router.push("/perfil");
    },
  },
};
</script>

<style scoped>
.leitura-container {
  width: 100%;
  display: flex;
  overflow: auto;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
}
.leitura-leitura {
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
/****************************************** */
.leitura1-rectangle6 {
  top: 178px;
  left: 639px;
  width: 643px;
  height: 574px;
  position: absolute;
  border-radius: 40px;
}
.leitura1-text19 {
  top: 800px;
  left: 639px;
  color: rgba(0, 0, 0, 1);
  width: 643px;
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
.leitura1-vector {
  top: 229px;
  left: 1089px;
  width: 41px;
  height: 45px;
  position: absolute;
}
.leitura1-frame3 {
  top: 414px;
  left: 778px;
  width: 200px;
  height: 247px;
  display: flex;
  overflow: hidden;
  position: absolute;
  align-items: flex-start;
  flex-shrink: 0;
  background-color: rgba(255, 255, 255, 1);
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
  border-radius: 50px;
  border: 10px solid black;
}
.leitura1-select {
  top: 230px;
  left: 820px;
  width: 254px;
  height: 40px;
  position: absolute;
  border-radius: 8px;
  padding: 8px;
  font-size: 16px;
}
.leitura-timer{
  top: 270px;
  left: 752px;
  width: 643px;
  height: 574px;
  position: absolute;
  flex-direction: column;
  display: flex;
  align-items: center;
  justify-content: center;
}
#timer {
  font-size: 5em;
  margin-bottom: 20px;
  color: #333;
  position: absolute;
  left: 40px;
  top: 28px;
}
#status {
  font-size: 1.2em;
  margin-bottom: 20px;
  color: #ececec;
  position: absolute;
  left: 256px;
  top: 144px;
}

#startBtn,
#pauseBtn,
#finalizeBtn {
  font-size: 1em;
  padding: 10px 20px;
  margin: 5px;
  cursor: pointer;
  border: none;
  border-radius: 5px;
  width: 140px;
  height: 59px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25) ;
}

#startBtn { background-color: #4CAF50; color: white; }
#pauseBtn { background-color: #FF9800; color: white; }
#finalizeBtn { background-color: #F44336; color: white; }
</style>
