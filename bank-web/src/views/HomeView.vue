<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import BankLayout from '../layouts/BankLayout.vue'
import Deposit from '../components/icons/Deposit.vue'
import Withdraw from '../components/icons/Withdraw.vue'
import Tranfer from '../components/icons/Tranfer.vue'
import Modal from '../components/Modal.vue'
import Toast from '../components/Toast.vue'


import { useUserStore } from "../stores/user"
import { useBankStore } from "../stores/bank"
import { useRouter } from "vue-router"

const router = useRouter();

const userStore = useUserStore();
const bankStore = useBankStore();

const userId = ref("");
const mode = ref("123")
const isOpenModal = ref(false);

const openModal = (m: string) => {
  mode.value = m
  isOpenModal.value = true
}

const closeModal = async (status: boolean, receiverId: string, amount: any) => {
  isOpenModal.value = false
  if(status){
    const data = {
      userId: userId.value,
      amount: parseInt(amount)
    }
    if(mode.value == 'Deposit'){
      await bankStore.deposit(data)
    }
    else if(mode.value == 'Withdraw') {
      await bankStore.withdraw(data)
    }
    else{
      const data = {
        userId: userId.value,
        amount: parseInt(amount),
        receiverId: receiverId
      }
      await bankStore.tranfer(data)
    }
    await bankStore.loadUser(data.userId)
  }
}

onMounted(() => {
  const token = localStorage.getItem('token');
  const UserId = localStorage.getItem('userId') ?? "";
  userId.value = UserId
  if(!(token && userId)) {
    router.push("/login")
  }
  bankStore.loadUser(UserId)
})
</script>

<template>
  <BankLayout>
    <div v-if="isOpenModal">
      <Modal :mode="mode" :closeModal="closeModal"/>
    </div>
    <div class="grid grid-cols-1 md:grid-cols-3 gap-8 p-8">
      <div class="shadow-xl p-8 flex items-center flex-row md:flex-col order-2 md:order-1 gap-6">
        <div class="flex-1" @click="openModal('Deposit')">
          <div class="w-full flex flex-col justify-center">
            <div class="border-color-black mb-3">
              <Deposit/>
            </div>
            <div class="">Deposit</div>
          </div>
        </div>  
        <div class="flex-1" @click="openModal('Withdraw')">
          <div class="border-color-black mb-3">
            <Withdraw/>
          </div>
          WithDraw
        </div>
        <div class="flex-1" @click="openModal('Tranfer')">
          <div class="border-color-black mb-3">
            <Tranfer/>
          </div>
          Tranfer
        </div>
      </div>
      <div class="col-span-1 md:col-span-2 shadow-xl p-8 order-1 md:order-2">
          <div class="">
              <div class="font-bold text-4xl mb-5">Welcome to Bank Web</div>
              <div class="text-2xl py-4">Name: {{ bankStore.user.firstName }} {{ bankStore.user.lastName }}</div>
              <div class="text-2xl py-4">Phone Number: {{ bankStore.user.phoneNumber }} </div>
              <div class="text-2xl py-4">Balance: {{ bankStore.user.currentBalance }} </div>
          </div>
      </div>
    </div>
    
  </BankLayout>>
</template>

<style>
.border-color-black{
  border: 1px solid black;
  border-radius: 100px;
  padding: 10px;
}

</style>