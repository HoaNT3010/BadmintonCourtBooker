import { createAsyncThunk } from "@reduxjs/toolkit";

export const checkIfLoggedIn = createAsyncThunk(
    "auth/verifyLogin",
    async(params, thunkAPI) => {
        try {
            // This call will hit the correct endpoint, that deserializes user and makes request to the database accordingly.
            // This works properly
            const result = await verifyLogin();
            
            if(!result) {
                throw new Error();
            }

            // If result returned something then return this promise
            return {
                user: {},
                isAuthenticated: true
            };

        } catch(e) {
            console.log(e.message);
            throw e
        }
    }
)

export const loginUser = createAsyncThunk(
    "auth/loginUser",
    async(params, thunkAPI) => {
        try {
            // This runs on user login
            const result = await userAuth(params);

            if(!result) {
                throw new Error();
            }

            return {
                user: result,
                cart: {},
                isAuthenticated: true
            }

        } catch(e) {
            console.log(e.message);
            throw e
        }
    }
)

const authSlice = createSlice({
    name: "auth",
    initialState: {
        isAuthenticated: false,
        data: {},
        isFetching: false,
        loadError: false
    },
    reducers: {
        // loadUser: (state, action) => {
        //     Object.assign(state, action.payload)
        //     console.log(state.user)
        // }, 

        toggleIsAuthenticated: (state, action) => {
            if(action.payload.status === 200){
                state.isAuthenticated = true
            }
            else {
                state.isAuthenticated = false
            }
            
            state.data = action.payload.data
            // state.isFetching = false;
        },
        // toggleIsFetching: (state, action) => {
        //     state.isFetching = true
        // },

        // toggleLoadError: (state, action) => {
        //     state.loadError = action.payload
        // }
    },
    extraReducers: (builder) => {
        builder
        .addCase(checkIfLoggedIn.fulfilled, (state, action) => {
            
            const { isAuthenticated, user } = action.payload;
            
            state.isAuthenticated = isAuthenticated;
            state.loadError = false;
            
            
        })
        .addCase(checkIfLoggedIn.rejected, (state, action)  => {
            state.loadError = true;
            state.isAuthenticated = false;
        })
        .addCase(loginUser.fulfilled, (state, action) => {
            const { isAuthenticated, user } = action.payload;
            state.isAuthenticated = isAuthenticated;
            state.loadError = false;
            
            
        })
        .addCase(loginUser.rejected, (state, action) => {
            state.loadError = true;
            state.isAuthenticated = false;
        })
    }

});


export default authSlice.reducer;
export const {toggleIsAuthenticated} = authSlice.actions;