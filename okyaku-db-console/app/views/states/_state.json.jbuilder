json.extract! state, :id, :name, :is_init, :is_accept, :state_machine_id, :created_at, :updated_at
json.url state_url(state, format: :json)
