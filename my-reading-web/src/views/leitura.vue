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
          <option v-for="livro in livros" :key="livro.id" :value="livro.id">{{ livro.titulo }}</option>
        </select>

        <div class="leitura-timer">
          <div id="timer">{{ formattedTime }}</div>
          <div id="status">{{ timerStatus }}</div>
          <button id="startBtn" @click="startTimer">Iniciar</button>
          <button id="pauseBtn" @click="togglePause">{{ isPaused ? "Despausar" : "Pausar" }}</button>
          <button id="finalizeBtn" @click="finalizeTimer">Finalizar</button>
        </div>

        <span class="leitura1-text19">{{ fraseMotivacional }}</span>
        <img src="/external/vector2614-od1c.svg" alt="Vector2614" class="leitura1-vector" />
        <div class="leitura1-frame3"></div>
      </main>
    </div>
  </div>
</template>

<script>
export default {
  name: "Leitura",
  data() {
    return {
      livros: [], // Lista de livros para ser preenchida do banco de dados
      livroSelecionado: "", // Livro selecionado pelo usuário
      fraseMotivacional: "Frase Motivadora",
      timerInterval: null,
      seconds: 0,
      isPaused: false,
      timerStatus: "Timer zerado",
      streakDays: 0, // Adiciona o valor da streak
    };
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
    goToBiblioteca() {
      this.$router.push("/biblioteca");
    },
    goToPerfil() {
      this.$router.push("/perfil");
    },
    gerarFraseMotivadora() {
      const frases = [
        `"A leitura é para a mente o que o exercício é para o corpo." — Joseph Addison`,
        `"Um livro é um sonho que você segura em suas mãos." — Neil Gaiman`,
        `"Ler é viajar sem sair do lugar."`,
        `"A leitura engrandece a alma." — Voltaire`,
        `"Um leitor vive mil vidas antes de morrer." — George R.R. Martin`,
        `"A leitura de um bom livro é um diálogo incessante: o livro fala e a alma responde." — André Maurois`,
        `"Cada livro é um mundo à espera de ser descoberto."`,
        `"Livros são amigos silenciosos e fiéis." — Confúcio`,
        `"A leitura nos dá um lugar para ir quando temos que ficar onde estamos." — Mason Cooley`,
        `"O livro é um mestre que fala, mas não responde." — Platão`,
      ];
      this.fraseMotivacional = frases[Math.floor(Math.random() * frases.length)];
    },
    startTimer() {
      if (this.livroSelecionado) {
        alert(`Iniciando leitura do livro ID: ${this.livroSelecionado}`);
        if (!this.timerInterval) {
          this.timerInterval = setInterval(this.updateTimer, 1000);
          this.timerStatus = "Em andamento";
          this.isPaused = false;
        }
      } else {
        alert("Por favor, selecione um livro.");
      }
    },
    togglePause() {
      if (this.timerInterval) {
        clearInterval(this.timerInterval);
        this.timerInterval = null;
        this.isPaused = true;
        this.timerStatus = "Timer pausado";
      } else if (this.isPaused) {
        this.startTimer(); // Reinicia o timer
      }
    },
    finalizeTimer() {
      if (this.timerInterval) {
        clearInterval(this.timerInterval);
        this.timerInterval = null;
      }
      alert(`Timer finalizado. Tempo total: ${this.formattedTime}`);
      this.timerStatus = "Timer zerado";
      this.seconds = 0;
      this.updateStreak(); // Atualiza a streak após finalizar o timer
    },
    updateTimer() {
      this.seconds++;
    },
    carregarLivros() {
      // Exemplo de dados estáticos; futuramente, utilize uma chamada à API/banco de dados para preencher essa lista
      this.livros = [
        { id: 1, titulo: "O Senhor dos Anéis" },
        { id: 2, titulo: "Dom Casmurro" },
        { id: 3, titulo: "O Alquimista" },
      ];
    },
    updateStreak() {
      const lastDate = localStorage.getItem('lastReadingDate');
      const today = new Date().toISOString().split('T')[0];

      if (lastDate) {
        const lastReading = new Date(lastDate);
        const difference = (new Date(today) - lastReading) / (1000 * 60 * 60 * 24);

        if (difference === 1) {
          // Leitura foi feita no dia anterior, incrementa a streak
          this.streakDays = parseInt(localStorage.getItem('streakDays')) + 1;
        } else if (difference > 1) {
          // Passaram-se mais de um dia, reseta a streak
          this.streakDays = 1;
        }
      } else {
        // Primeira leitura, inicia a streak
        this.streakDays = 1;
      }

      // Salva o valor atualizado da streak e a data de hoje
      localStorage.setItem('streakDays', this.streakDays);
      localStorage.setItem('lastReadingDate', today);
    },
  },
  mounted() {
    this.gerarFraseMotivadora();
    this.carregarLivros(); // Carrega os livros ao montar o componente
    this.streakDays = parseInt(localStorage.getItem('streakDays')) || 0;
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
  top: 317px;
  left: 822px;
  width: 277px;
  height: 246px;
  display: flex;
  overflow: hidden;
  position: absolute;
  align-items: flex-start;
  flex-shrink: 0;
  background-color: rgba(255, 255, 255, 1);
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
  top: 646px;
  left: 732px;
  width: 638px;
  height: 574px;
  position: absolute;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
#timer {
  font-size: 2em;
  margin-bottom: 20px;
  color: #333;
  position: absolute;
  left: 156px;
  top: -66px;
}
#status {
  font-size: 1.2em;
  margin-bottom: 20px;
  color: #ececec;
  position: absolute;
  left: 166px;
  top: -26px;
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
