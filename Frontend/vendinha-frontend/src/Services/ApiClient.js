import axios from "axios";

export default function ApiClient() {
    const baseAddress = "https://localhost:7156/api/";

    return ({
        //CLIENTES REGION
        GetClientes: function (page, filteredName) {
            let queryParams = '?';
            if (page > 0) {
                queryParams += 'page=' + page;
            }
            if (filteredName != null && filteredName !== '') {
                queryParams += '&filteredName=' + filteredName;
            }
            return axios.get(baseAddress + 'clientes' + queryParams);
        },
        CadastrarCliente: function (nome, cpf, dataNascimento, email) {
            return axios.post(baseAddress + 'Clientes', { nome: nome, cpf: cpf, dataNascimento: dataNascimento, email: email });
        },
        AlterarCliente: function (id, nome, cpf, dataNascimento, email) {
            return axios.put(baseAddress + 'Clientes', { id: id, nome: nome, cpf: cpf, dataNascimento: dataNascimento, email: email });
        },
        ApagarCliente: function (id) {
            return axios.delete(baseAddress + 'clientes/' + id);
        },
        //DÍVIDAS REGION
        GetDividasPorCliente: function (id) {
            return axios.get(baseAddress + 'Dividas/clientes/' + id);
        },
        CadastrarDivida: function (id, valor) {
            return axios.post(baseAddress + 'Dividas/', { clienteId: id, valor: valor });
        },
        PagarDivida: function (id) {
            return axios.patch(baseAddress + 'Dividas/' + id);
        }
        // //Token
        // RequestAuthToken: function (token) {
        //     return axios.post(baseAddress + 'Auth/token', { token: token })
        // }
    })
}