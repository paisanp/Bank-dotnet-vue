<script setup lang="ts">
import { ref, onMounted } from 'vue';
import BankLayout from '../layouts/BankLayout.vue'


import { useUserStore } from "../stores/user"
import { useBankStore } from "../stores/bank"
import { useRouter } from "vue-router"

const router = useRouter();

const userStore = useUserStore();
const bankStore = useBankStore();

const userId = ref("");
const type = ref(true);

const findFullName = (id: string) => {
    const user = bankStore.users.find(x => x.userId === id)
    return user.firstName + ' ' + user.lastName
}

const switchType = () => {
    const data = {
        userId: userId.value ,
        type: type.value ? "S" : "E"
    }
    bankStore.loadHistoryTransaction(data)

}

onMounted(() => {
  const token = localStorage.getItem('token');
  userId.value = localStorage.getItem('userId') ?? "";

  if(!(token && userId)) {
    router.push("/login")
  }
  const data = {
    userId: userId.value ,
    type: type.value ? "S" : "E"
  }
  console.log('data', data)
  bankStore.loadHistoryTransaction(data)
  bankStore.loadAllUser()
})


</script>

<template>
  <BankLayout>
    <div class="p-8">
        <div class="flex items-center">
            <div class="font-bold text-2xl mb-5 mr-6">History Transacion</div>
            <div class="flex gap-3">
                <div>Receiver</div>
                <input type="checkbox" class="toggle" v-model="type" @change="switchType"/>
                <div>Senter</div>
            </div>
        </div>
        <div class="overflow-x-auto mb-5">
            <table class="table">
                <!-- head -->
                <thead>
                <tr>
                    <th></th>
                    <th>Type</th>
                    <th>Sender</th>
                    <th>Receive</th>
                    <th>amount</th>
                    <th>remain</th>
                    <th>date</th>
                </tr>
                </thead>
                <tbody>
                <!-- row 1 -->
                <tr v-for=" (h, index) in bankStore.historyTransaction">
                    <th>{{ index + 1}}</th>
                    <th>{{h.type}}</th>
                    <td>{{ findFullName(h.senderId) }}</td>
                    <td>{{ findFullName(h.receiverId) }}</td>
                    <td>{{ h.amount }}</td>
                    <td>{{ h.remain }}</td>
                    <td>{{ h.date}}</td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
  </BankLayout>
</template>
