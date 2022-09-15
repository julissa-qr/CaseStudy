import { createAsyncThunk, isAnyOf } from "@reduxjs/toolkit";
import { createSlice } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";
import { Navigate, useNavigate } from "react-router";
import { toast } from "react-toastify";
import agent from "../../api/agent";
import { User } from "../../models/user";

interface AccountState {
    user: User | null;
}

const initialState: AccountState = {
    user: null
}

export const signInUser = createAsyncThunk<User, FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try {
            const user = await agent.Account.login(data);
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
)

export const fetchCurrentUser = createAsyncThunk<User>(
    'account/fetchCurrentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user')!)));
        try {
            const user = await agent.Account.currentUser();
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    },
    {
        condition: () => {
            if (!localStorage.getItem('user')) {
                return false;
            };
        }
    }

)

export const accountSlice = createSlice({

    name: 'account',
    initialState,
    reducers: {
        signOut: (state) => {
            state.user = null;
            localStorage.removeItem('user');
        },
        setUser: (state, action) => {
            state.user = action.payload;
        }
    },



    extraReducers: (builder => {
        builder.addCase(fetchCurrentUser.rejected, (state) => {
            state.user = null;
            localStorage.removeItem('user');
            //const navigate = useNavigate();
            toast.error('Session expired - please login again');
            //navigateLogin();
            
        })

        builder.addMatcher(isAnyOf(signInUser.fulfilled, fetchCurrentUser.fulfilled), (state, action) => {
            state.user = action.payload;
        });
        builder.addMatcher(isAnyOf(signInUser.rejected), (state, action) => {
            console.log(action.payload);
        })
    })
})

export const { signOut, setUser } = accountSlice.actions;