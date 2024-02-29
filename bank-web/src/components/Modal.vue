<script setup>
import { ref, defineProps, onMounted } from 'vue'
import Phone from '../components/icons/Phone.vue'
import Money from '../components/icons/Money.vue'

import { useBankStore } from "../stores/bank"


defineProps({
    mode: String,
    closeModal: Function
})

const bankStore = useBankStore();


const receiverId = ref("")
const amount = ref("")

const action = () => {
    closeModal()
}
onMounted(() =>{
    bankStore.loadAllUser()
})
</script>
<template> 

    <!-- Put this part before </body> tag -->
    <input type="checkbox" id="my_modal_6" class="modal-toggle" checked/>
    <div class="modal" role="dialog">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{mode}}!</h3>
                <div class="">
                    <label v-if="mode =='Tranfer'" class="input input-bordered flex items-center gap-2 mt-6" >
                        <div class="size-6">
                            <Phone/>
                        </div>
                        <select class="grow" style="background-color: transparent;" v-model="receiverId">
                        <option disabled selected>Phone Number?</option>
                        <option :value="user.userId" v-for="user in bankStore.users">{{ user.phoneNumber }} - {{ user.firstName }} {{ user.lastName }}</option>
                        </select>
                    </label>
                    
                    <label class="input input-bordered flex items-center gap-2 mt-6">
                        <div class="size-6">
                            <Money/>
                        </div>
                        <input type="int" class="grow" placeholder="Amount" v-model="amount"/>
                    </label>
                </div>
            <div class="modal-action">
                <label for="my_modal_6" class="btn btn-neutral" @click="closeModal(true, receiverId, amount)">{{mode}}!</label>

                <label for="my_modal_6" class="btn" @click="closeModal(false, receiverId, amount)">Close!</label>
            </div>
        </div>
    </div>
</template>
