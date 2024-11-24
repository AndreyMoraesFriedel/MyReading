<template>
  <div class="biblioteca-container">
    <div class="biblioteca-biblioteca">
      <!--NAV-->
      <header>
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
              <li style="list-style-type: none">
                <img
                  src="/external/IconedeBiblioteca.png"
                  alt="Icone de Biblioteca"
                  class="nav-icone-de-biblioteca"
                />
                <span class="nav-texto-biblioteca">Biblioteca</span>
              </li>
              <li style="list-style-type: none">
                <button @click="irParaLeitura" style="cursor: pointer;">
                  <img
                    src="/external/IconedeLeitura.png"
                    alt="Icone de Leitura"
                    class="nav-icone-de-leitura"
                  />
                  <span class="nav-texto-leitura">Leitura</span>
                </button>
              </li>
              <li style="list-style-type: none">
                <button @click="irParaPerfil" style="cursor: pointer;">
                  <img
                    src="/external/IconedePerfil.png"
                    alt="Icone de Perfil"
                    class="nav-icone-de-perfil"
                  />
                  <span class="nav-texto-perfil">Perfil</span>  
                </button>
              </li>
            </ul>
          </nav>
        <div class="streak">
            <img
              src="/external/livrostreak.svg"
              alt="LivroDoStreak"
              class="streak-livro"
            />
            <span class="streak-numero">{{ streakDays }}</span>
        </div>
      </header>
      <!--PARTE DEBAIXO-->
      <main>
        <span class="biblioteca-text3">Minha Lista de Livros:</span>

        <!-- Botão de Adicionar Livro -->
        <button style="cursor: pointer;" @click="abrirFormulario">
          <img
            src="/external/rectangle48210-tewv.svg"
            alt="Rectangle48210"
            class="biblioteca-rectangle4"
          />
          <img
            src="/external/rectangle58210-6chk-200h.png"
            alt="Rectangle58210"
            class="biblioteca-rectangle5"
          />
          <img
            src="/external/pluscircle8211-3h5.svg"
            alt="Pluscircle8211"
            class="biblioteca-pluscircle"
          />
          <span class="biblioteca-text9">Adicione um Livro</span>
        </button>

         <!-- Exibir as capas dos livros -->
          <div class="livros-container">
            <div v-for="livro in livrosDoUsuario" :key="livro.id" class="livro">
              <img :src="livro.capaUrl" :alt="livro.title" class="livro-capa" />
              <p class="livro-title">{{ livro.title }}</p>
            </div>
          </div>

        <!-- Formulário para Adicionar Livro -->
        <div v-if="exibirFormulario" class="biblioteca-form-popup">
          <h2>Cadastro de Livro</h2>
          <form @submit.prevent="cadastrarLivro">
            <label for="biblioteca-bookName">Nome do Livro:</label>
            <input type="text" v-model="livro.nome" id="biblioteca-bookName" required />

            <label for="biblioteca-author">Autor:</label>
            <input type="text" v-model="livro.autor" id="biblioteca-author" required />

            <label for="biblioteca-totalPages">Total de Páginas:</label>
            <input type="number" v-model="livro.paginas" id="biblioteca-totalPages" required />

            <label for="biblioteca-coverImage">Capa do Livro:</label>
            <input type="file" @change="selecionarImagem" id="biblioteca-coverImage" accept="image/*" />

            <button type="submit" id="biblioteca-submitBtn">Cadastrar Livro</button>
            <button type="button" id="biblioteca-closeFormBtn" @click="fecharFormulario">Cancelar</button>
          </form>
          <p v-if="mensagemConfirmacao" class="biblioteca-confirmationMessage">Livro cadastrado!</p>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import axios from '../http-common';

export default {
  name: 'Biblioteca',
  data() {
    return {
      exibirFormulario: false,
      mensagemConfirmacao: false,
      livrosDoUsuario: [], // Lista de livros do usuário
      livro: {
        nome: '',
        autor: '',
        paginas: '',
        capa: null,
      },
      streakDays: 0,
    };
  },
  metaInfo: {
    title: 'Biblioteca',
  },
  created() {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.obterStreakDoUsuario(userId);
      this.carregarLivrosDoUsuario(userId); // Carregar os livros ao abrir a página
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
        return null; // Retorna nulo em caso de erro
      }
    },
    irParaPerfil() {
      this.$router.push('/Perfil');
    },
    irParaLeitura() {
      this.$router.push('/leitura');
    },
    abrirFormulario() {
      this.exibirFormulario = true;
      this.mensagemConfirmacao = false;
    },
    fecharFormulario() {
      this.exibirFormulario = false;
      this.mensagemConfirmacao = false;
      this.resetarFormulario();
    },
    async cadastrarLivro() {
      if (this.livro.nome && this.livro.autor && this.livro.paginas && this.livro.capa) {
        try {
          const formData = new FormData();
          formData.append('Title', this.livro.nome);
          formData.append('Author', this.livro.autor);
          formData.append('Pages', this.livro.paginas);
          formData.append('Capa', this.livro.capa);

          const response = await axios.post('/api/v1/book', formData);
          const bookId = response.data.id;
          const userIdInt = Number(localStorage.getItem('userId'));

          await axios.post(`/api/v1/reading-progress`, {
            userId: userIdInt,
            bookId: bookId,
          });

          this.mensagemConfirmacao = true;
          this.resetarFormulario();

          // Atualizar lista de livros após cadastro
          this.carregarLivrosDoUsuario(userIdInt);
        } catch (error) {
          console.error('Erro ao cadastrar livro ou iniciar progresso:', error.response || error.message);
          alert('Erro ao cadastrar o livro ou iniciar o progresso. Tente novamente.');
        }
      } else {
        alert('Por favor, preencha todos os campos obrigatórios.');
      }
    },
    selecionarImagem(event) {
      this.livro.capa = event.target.files[0];
    },
    resetarFormulario() {
      this.livro = {
        nome: '',
        autor: '',
        paginas: '',
        capa: null,
      };
    },
  },
};
</script>


<style scoped>
.biblioteca-container {
  width: 100%;
  display: flex;
  overflow: auto;
  min-height: 100vh;
  align-items: center;
  flex-direction: column;
}
.biblioteca-biblioteca {
  width: 100%;
  height: 1080px;
  display: flex;
  overflow: hidden;
  position: relative;
  align-items: center;
  flex-shrink: 0;
  justify-content: flex-start;
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
/***************************************** */
.biblioteca-text3 {
  top: 186px;
  left: 70px;
  color: rgba(0, 0, 0, 1);
  height: auto;
  position: absolute;
  font-size: 40px;
  font-style: Regular;
  text-align: justified;
  font-family: Inter;
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
.biblioteca-rectangle4 {
  top: 258px;
  left: 67px;
  width: 211px;
  height: 253px;
  position: absolute;
}
.biblioteca-rectangle5 {
  top: 258px;
  left: 72px;
  width: 200px;
  height: 130px;
  position: absolute;
  border-radius: 40px;
}
.biblioteca-pluscircle {
  top: 279px;
  left: 133px;
  width: 80px;
  height: 76px;
  position: absolute;
}
.biblioteca-open-book1 {
  top: 171px;
  left: 3266px;
  width: 73px;
  height: 49px;
  position: absolute;
}
.biblioteca-text9 {
  top: 424px;
  left: 88px;
  color: rgb(255, 255, 255);
  width: 169px;
  height: auto;
  position: absolute;
  font-size: 19px;
  font-style: Regular;
  text-align: center;
  font-family: "Inter";
  font-weight: 400;
  line-height: 100%;
  font-stretch: normal;
  text-decoration: none;
}
/*
#biblioteca-submitBtn,
#biblioteca-closeFormBtn {
    padding: 15px 25px;
    margin: 5px;
    font-size: 16px;
    cursor: pointer;
    background-color: #333;
    color: #fff;
    border: none;
    border-radius: 12px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
    transition: transform 0.2s ease;
}
*/
button:hover {
    transform: scale(1.05);
}

.biblioteca-form-popup {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    padding: 20px;
    width: 350px;
    border-radius: 12px;
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.4);
    z-index: 10;
}

.biblioteca-form-popup h2 {
    margin-top: 0;
    color: #333;
}

#biblioteca-form {
  width: 300px;
  height: 100%;
  padding: 8px;
}

input[type="text"],
input[type="number"],
input[type="file"] {
    width: 292px;
    padding: 10px;
    margin: 10px 0 15px;
    border: 1px solid #ccc;
    border-radius: 8px;
    font-size: 14px;
}

#biblioteca-submitBtn, #biblioteca-closeFormBtn {
    width: 100%;
    padding: 10px;
    margin-top: 10px;
    font-size: 16px;
    cursor: pointer;
    border: none;
    border-radius: 8px;
    color: #fff;
}

#biblioteca-submitBtn {
    background-color: #333;
}

#biblioteca-closeFormBtn {
    background-color: #777;
}

#biblioteca-submitBtn:hover, #biblioteca-closeFormBtn:hover {
    opacity: 0.9;
}

#biblioteca-confirmationMessage {
    color: green;
    font-weight: bold;
}
.livros-container {
  display: flex;
  flex-wrap: wrap;
  gap: 100px;
  margin-top: 20px;
  position: absolute;
  left: 330px;
  top: 240px;
}

.livro {
  text-align: center;
  max-width: 150px;
}

.livro-capa {
  width: 200px;
  height: 247px;
  object-fit: cover; /* Para cortar e ajustar a imagem */
  border-radius: 8px;
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
  border-radius: 50px;
  border: 10px solid black;
}

.livro-title {
  margin-top: 8px;
  font-size: 14px;
  font-weight: bold;
  color: #333;
}
</style>
