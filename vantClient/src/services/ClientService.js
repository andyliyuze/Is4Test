import axios from 'axios'
import Mgr from './SecurityService'
import 'babel-polyfill';

const baseUrl = 'http://192.168.43.149:9000/api/';
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
                .get(baseUrl + "client/getList", { params: data })
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
                .get(baseUrl + "apiResource/getAllScopes",)
                .then(response => resolve(response.data.result))
                .catch(err => {
                    console.log(err);
                })
        })
    }


}