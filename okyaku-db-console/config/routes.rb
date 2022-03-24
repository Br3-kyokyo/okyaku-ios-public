Rails.application.routes.draw do

  root to: 'welcome#index'
  
  resources :state_machines, shallow: true do
    resources :states
    resources :transitions
  end
  
  resources :transitions do
    resource :customer_action
    resource :trigger
  end
  
  resources :triggers, shallow: true do
    resources :trigger_sentences
  end

  resources :scenario_categories
  
  # For details on the DSL available within this file, see https://guides.rubyonrails.org/routing.html
end
