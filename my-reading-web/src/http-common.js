import Axios from 'axios';

const createAxios = Axios.create({
    baseURL: "https://localhost:7172"
});

export default createAxios;