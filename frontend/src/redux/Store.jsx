import { configureStore } from "@reduxjs/toolkit";
import authReducer from '../features/Auth/data/Slice'
export default configureStore(
    {
        reducer : { 
            auth: authReducer,
        }
    },
);