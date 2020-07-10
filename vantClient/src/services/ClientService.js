import axios from 'axios'
import Mgr from './SecurityService'
import 'babel-polyfill';

const baseUrl = process.env.VUE_APP_IS4URL + "/api";
var user = new Mgr()

export default class ClientService {

    async defineHeaderAxios() {
        await user.getAcessToken().then(
            acessToken => {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + acessToken
            }, err => {
                console.log(err)
            })
    }

    async getClients(data) {
        await this.defineHeaderAxios()
        return new Promise((resolve) => {
            axios
                .get(baseUrl + "/client/getList", { params: data })
                .then(response => resolve(response.data.result))
                .catch(err => {
                    console.log(err);
                })
        })
    }

    async getAllScopes() {
        await this.defineHeaderAxios()
        return new Promise((resolve) => {
            axios
                .get(baseUrl + "/apiResource/getAllScopes",)
                .then(response => resolve(response.data.result))
                .catch(err => {
                    console.log(err);
                })
        })
    }

    async getClientTypes() {
        await this.defineHeaderAxios()
        return new Promise((resolve) => {
            axios
                .get(baseUrl + "/client/getClientTypes",)
                .then(response => resolve(response.data.result))
                .catch(err => {
                    console.log(err);
                })
        })
    }

    async createClient(data) {
        await this.defineHeaderAxios()
        return new Promise((resolve) => {
            axios
                .post(baseUrl + "/client/create", data)
                .then(response => resolve(response.data))
                .catch(err => {
                    console.log(err);
                })
        })
    }
}