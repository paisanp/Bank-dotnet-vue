import { defineStore } from 'pinia'
import axios from 'axios'
import type { Ref } from 'vue';
import { useRouter } from "vue-router"


const BASE_URL = 'https://localhost:7284/api/Bank';
const router = useRouter();

export const useBankStore = defineStore('bank', {
    state: () => ({
        users: [],
        user: {},
        historyTransaction: [],
    }),
    actions: {
        async loadUser (userId: string) {
            try{
                const response = await axios.get(`${BASE_URL}/GetUserById/${userId}`, {
                    headers: {
                        "charset": 'utf-8',
                        "Authorization": "Bearer " + localStorage.getItem('token'),
                    }
                })
                this.user = response.data
            }
            catch (error){
                console.log('error: ', error)
                router.push("/login")
            }
        },
        async loadHistoryTransaction (data: { userId: any; type: any; }) {
            try{
                const response = await axios.get(`${BASE_URL}/GetHistoryTransaction?UserId=${data.userId}&type=${data.type}`, {
                    headers: {
                        "charset": 'utf-8',
                        "Authorization": "Bearer " + localStorage.getItem('token'),
                    }
                })
                this.historyTransaction = response.data
            }
            catch (error){
                router.push("/login")
                console.log('error: ', error)
            }
        },
        async loadAllUser () {
            try{
                const response = await axios.get(`${BASE_URL}/GetAllUser`, {
                    headers: {
                        "charset": 'utf-8',
                        "Authorization": "Bearer " + localStorage.getItem('token'),
                    }
                })
                this.users = response.data
            }
            catch (error){
                router.push("/login")
                console.log('error: ', error)
            }
        },
        async deposit (data: { userId: string; amount: number; }) {
            try{
                const response = await axios.post(`${BASE_URL}/Deposit`, data)
                this.users = response.data
            }
            catch (error){
                router.push("/login")
                console.log('error: ', error)
            }
        },
        async withdraw (data: { userId: string; amount: number; }) {
            try{
                const response = await axios.post(`${BASE_URL}/WithDraw`, data)
                this.users = response.data
            }
            catch (error){
                router.push("/login")
                console.log('error: ', error)
            }
        },
        async tranfer (data: { userId: string; amount: number; receiverId: string;}) {
            try{
                const response = await axios.post(`${BASE_URL}/Tranfer`, data)
                this.users = response.data
            }
            catch (error){
                router.push("/login")
                console.log('error: ', error)
            }
        },
        
    }
})