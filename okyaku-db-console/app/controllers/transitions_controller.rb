class TransitionsController < ApplicationController
  before_action :set_transition, only: [:show, :edit, :update, :destroy]
  before_action :set_state_machine

  # GET /transitions
  # GET /transitions.json
  def index
    @transitions = Transition.all
  end

  # GET /transitions/1
  # GET /transitions/1.json
  def show
    @action = @transition.customer_action
    @trigger = @transition.trigger
  end

  # GET /transitions/new
  def new
    @transition = Transition.new
    @transition.build_customer_action
    @transition.build_trigger
    @transition.trigger.sentences.build
  end

  # GET /transitions/1/edit
  def edit
  end

  # POST /transitions
  # POST /transitions.json
  def create
    @transition = Transition.new(transition_params)

    respond_to do |format|
      if @transition.save
        format.html { redirect_to state_machine_path(@state_machine), notice: 'Transition was successfully created.' }
        format.json { render :show, status: :created, location: @transition }
      else
        format.html { render :new }
        format.json { render json: @transition.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /transitions/1
  # PATCH/PUT /transitions/1.json
  def update
    respond_to do |format|
      if @transition.update(transition_params)
        format.html { redirect_to state_machine_path(@state_machine), notice: 'Transition was successfully updated.' }
        format.json { render :show, status: :ok, location: @transition }
      else
        format.html { render :edit }
        format.json { render json: @transition.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /transitions/1
  # DELETE /transitions/1.json
  def destroy
    @transition.destroy
    respond_to do |format|
      format.html { redirect_to state_machine_path(@state_machine), notice: 'Transition was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_transition
      @transition = Transition.find(params[:id])
    end

    def set_state_machine
      @state_machine = @transition&.state_machine || StateMachine.find(params[:state_machine_id])
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def transition_params
      params.require(:transition).permit(:name, :state_machine_id, :prev_state_id, :next_state_id, customer_action_attributes: [:id, :name, :text_en, :text_ja, :_destroy], trigger_attributes: [:id, :name, { keywords_attributes: [:id, :word, :_destroy], sentences_attributes: [:id, :position, :body_en, :body_ja, :_destroy]}, :_destroy])
    end
end
