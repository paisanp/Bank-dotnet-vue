<script setup>

import { ref, watch } from "vue"
import { useUserStore } from "../stores/user"
import { useRouter } from "vue-router"

const username = ref("")
const password = ref("")

const router = useRouter();

const userStore = useUserStore();

const onLogin = async () => {
    const data = {
        username: username.value,
        password: password.value
    }
    await userStore.onLogin(data)


    if(userStore.isLogin){
        router.push('/')
    }
}

</script>

<template>
    <div class="h-screen flex items-center">
        <div class="flex-1 max-w-3xl mx-auto shadow-xl p-8">
            <div class="flex flex-col items-center">
                <div class="font-bold text-4xl mb-5">Login Page</div>
                <label class="w-full input input-bordered flex items-center gap-2 mb-4">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70"><path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" /></svg>
                    <input type="text" class="grow" placeholder="Username" v-model="username"/>
                </label>
                
                <label class="w-full input input-bordered flex items-center gap-2 mb-4">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70"><path fill-rule="evenodd" d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z" clip-rule="evenodd" /></svg>
                    <input type="password" class="grow" placeholder="Password" v-model="password"/>
                </label>
                <button @click="onLogin()" class="btn btn-neutral w-full">Login</button>
            </div>
        </div>
    </div>
</template>